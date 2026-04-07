
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;
using MyShop_v2.Domain.Enums;

namespace MyShop_v2.Application.Filters
{
    public class FilterService
    {
        public Expression<Func<T, bool>> BuildPredicate<T>(FilterGroup filterGroup)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var expression = BuildGroupExpression<T>(filterGroup, parameter);
            return Expression.Lambda<Func<T, bool>>(expression, parameter);
        }

        private Expression BuildGroupExpression<T>(FilterGroup group, ParameterExpression parameter)
        {
            var expressions = new List<Expression>();

            foreach (var c in group.Conditions)
            {
                expressions.Add(BuildConditionExpression<T>(c, parameter));
            }

            foreach (var subGroup in group.Groups)
            {
                expressions.Add(BuildGroupExpression<T>(subGroup, parameter));
            }

            if (!expressions.Any())
            {
                return group.Operator.ToUpper() == "AND" ? Expression.Constant(true) : Expression.Constant(false);
            }

            return group.Operator.ToUpper() == "AND"
                ? expressions.Aggregate(Expression.AndAlso)
                : expressions.Aggregate(Expression.OrElse);
        }

        private Expression BuildConditionExpression<T>(FilterCondition condition, ParameterExpression parameter)
        {
            Expression propertyExpression = parameter;
            Type currentType = typeof(T);

            var fieldParts = condition.Field.Split('.');
            foreach (var part in fieldParts)
            {
                PropertyInfo? propertyInfo = currentType.GetProperty(part, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (propertyInfo == null)
                {
                    throw new ArgumentException($"Property '{part}' not found on type '{currentType.Name}' for field '{condition.Field}'.");
                }
                propertyExpression = Expression.Property(propertyExpression, propertyInfo);
                currentType = propertyInfo.PropertyType;
            }

            Type targetType = currentType;
            object? value = ExtractValue(condition.Value, targetType);

            Condition cond;
            try
            {
                cond = (Condition)Enum.Parse(typeof(Condition), condition.Operator.ToUpper(), true);
            }
            catch (ArgumentException)
            {
                throw new NotSupportedException($"Operator '{condition.Operator}' is not supported or invalid.");
            }

            if (cond == Condition.BETWEEN)
            {
                if (propertyExpression is not MemberExpression memberProperty)
                {
                    throw new InvalidOperationException($"BETWEEN operator requires a direct property access (MemberExpression), but found {propertyExpression.GetType().Name}.");
                }
                return BuildBetweenExpression(memberProperty, value);
            }

            Type constantType = Nullable.GetUnderlyingType(targetType) ?? targetType;
            var constantValue = Expression.Constant(value, constantType);

            return cond switch
            {
                Condition.EQ => Expression.Equal(propertyExpression, constantValue),
                Condition.NEQ => Expression.NotEqual(propertyExpression, constantValue),
                Condition.GT => Expression.GreaterThan(propertyExpression, constantValue),
                Condition.LT => Expression.LessThan(propertyExpression, constantValue),
                Condition.CONTAINS => BuildStringContainsExpression(propertyExpression, constantValue),
                Condition.LIKE => BuildStringContainsExpression(propertyExpression, constantValue),
                Condition.IN => BuildInExpression(propertyExpression, value, targetType),
                _ => throw new NotSupportedException($"Operator '{condition.Operator}' is not supported.")
            };
        }

        private Expression BuildStringContainsExpression(Expression propertyExpression, ConstantExpression constantValue)
        {
                if (propertyExpression.Type != typeof(string))
            {
                throw new InvalidOperationException("CONTAINS/LIKE can only be used with string properties.");
            }

            var notNull = Expression.NotEqual(propertyExpression, Expression.Constant(null, typeof(string)));

            var toLowerMethod = typeof(string).GetMethod("ToLower", Type.EmptyTypes);
            var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });

            if (toLowerMethod == null || containsMethod == null)
            {
                throw new InvalidOperationException("Required string methods (ToLower, Contains) not found.");
            }

            var lowerProperty = Expression.Call(propertyExpression, toLowerMethod);
            var lowerConstant = Expression.Constant(constantValue.Value?.ToString()?.ToLower(), typeof(string));

            var containsCall = Expression.Call(lowerProperty, containsMethod, lowerConstant);

            return Expression.AndAlso(notNull, containsCall);
        }

        private Expression BuildInExpression(Expression propertyExpression, object? value, Type targetType)
        {
            if (value is not System.Collections.IEnumerable list)
            {
                throw new InvalidOperationException("IN operator requires a list of values.");
            }

            var typedList = (System.Collections.IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(targetType))!;

            foreach (var item in list)
            {
                try
                {
                    typedList.Add(Convert.ChangeType(item, targetType));
                }
                catch (Exception ex)
                {
                    throw new ArgumentException($"Could not convert item '{item}' in IN list to target type '{targetType.Name}'.", ex);
                }
            }

            var containsMethod = typeof(Enumerable).GetMethods()
                .Where(m => m.Name == "Contains" && m.IsGenericMethodDefinition)
                .Select(m => m.MakeGenericMethod(targetType))
                .FirstOrDefault(m => m.GetParameters().Length == 2);

            if (containsMethod == null)
            {
                throw new InvalidOperationException("Enumerable.Contains method not found.");
            }

            var listConstant = Expression.Constant(typedList, typedList.GetType());

            return Expression.Call(containsMethod, listConstant, propertyExpression);
        }

        private Expression BuildBetweenExpression(MemberExpression property, object? value)
        {
            if (value is not IList<object> range || range.Count != 2)
                throw new ArgumentException("BETWEEN operator requires exactly two values (a lower and an upper bound).");

            var lowerValue = range[0];
            var upperValue = range[1];

            var lower = Expression.Constant(Convert.ChangeType(lowerValue, property.Type), property.Type);
            var upper = Expression.Constant(Convert.ChangeType(upperValue, property.Type), property.Type);

            return Expression.AndAlso(
                Expression.GreaterThanOrEqual(property, lower),
                Expression.LessThanOrEqual(property, upper)
            );
        }

        private object? ExtractValue(object? input, Type targetType)
        {
            Type actualTargetType = Nullable.GetUnderlyingType(targetType) ?? targetType;

            if (input == null)
            {
                return null;
            }

            if (input is JsonElement jsonElement)
            {
                try
                {
                    if (jsonElement.ValueKind == JsonValueKind.String)
                    {
                        if (actualTargetType == typeof(string)) return jsonElement.GetString();
                        if (actualTargetType == typeof(DateTime)) return jsonElement.GetDateTime();
                        if (actualTargetType.IsEnum) return Enum.Parse(actualTargetType, jsonElement.GetString()!, ignoreCase: true);
                        return Convert.ChangeType(jsonElement.GetString(), actualTargetType);
                    }
                    else if (jsonElement.ValueKind == JsonValueKind.Number)
                    {
                        if (actualTargetType == typeof(int)) return jsonElement.GetInt32();
                        if (actualTargetType == typeof(long)) return jsonElement.GetInt64();
                        if (actualTargetType == typeof(decimal)) return jsonElement.GetDecimal();
                        if (actualTargetType == typeof(double)) return jsonElement.GetDouble();
                        if (actualTargetType == typeof(float)) return jsonElement.GetSingle();
                    }
                    else if (jsonElement.ValueKind == JsonValueKind.True || jsonElement.ValueKind == JsonValueKind.False)
                    {
                        if (actualTargetType == typeof(bool)) return jsonElement.GetBoolean();
                    }
                    else if (jsonElement.ValueKind == JsonValueKind.Null)
                    {
                        return null;
                    }
                    else if (jsonElement.ValueKind == JsonValueKind.Array)
                    {
                        var list = new List<object>();
                        foreach (var element in jsonElement.EnumerateArray())
                        {
                            list.Add(ExtractValue(element, actualTargetType)!);
                        }
                        return list;
                    }

                    return Convert.ChangeType(jsonElement.ToString(), actualTargetType);
                }
                catch (Exception ex)
                {
                    throw new ArgumentException($"Could not convert JSON value '{jsonElement}' (kind: {jsonElement.ValueKind}) to target type '{targetType.Name}'.", ex);
                }
            }

            try
            {
                return Convert.ChangeType(input, actualTargetType);
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Could not convert value '{input}' of type '{input.GetType().Name}' to target type '{targetType.Name}'.", ex);
            }
        }



    }
}