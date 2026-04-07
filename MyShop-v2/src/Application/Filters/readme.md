# 🧠 Dynamic Filter Service Documentation

## 📌 Overview

The `FilterService` is a dynamic query builder that converts user-defined filters into **LINQ expression trees**.

It allows you to:

* Build flexible and reusable filtering logic
* Accept dynamic filters from APIs (JSON)
* Apply them directly to database queries (e.g., Entity Framework)

---

## 🔄 How It Works (Flow)

```
FilterGroup (JSON/API input)
        ↓
FilterService
        ↓
Expression Tree
        ↓
Lambda (x => condition)
        ↓
LINQ Query (.Where)
        ↓
Filtered Results
```

---

## 🧩 Core Concepts

### FilterCondition

Represents a single rule.

```json
{
  "field": "Age",
  "operator": ">",
  "value": 30
}
```

---

### FilterGroup

Represents a group of conditions combined with `AND` / `OR`.

```json
{
  "operator": "AND",
  "conditions": [...],
  "groups": [...]
}
```

---

## ⚙️ FilterService Methods

---

### 1. `BuildPredicate<T>`

```csharp
public Expression<Func<T, bool>> BuildPredicate<T>(FilterGroup filterGroup)
```

**Purpose:**

* Entry point of the filter system
* Converts a `FilterGroup` into a LINQ-compatible predicate

**What it does:**

* Creates a parameter (`x`)
* Builds the full expression tree
* Wraps it into a lambda: `x => condition`

**Usage:**

```csharp
var predicate = filterService.BuildPredicate<Product>(filterGroup);
var results = dbContext.Products.Where(predicate).ToList();
```

---

### 2. `BuildGroupExpression<T>`

```csharp
private Expression BuildGroupExpression<T>(FilterGroup group, ParameterExpression parameter)
```

**Purpose:**

* Combines multiple conditions and subgroups

**What it does:**

* Recursively processes:

  * Conditions
  * Nested groups
* Joins them using:

  * `AND` → `Expression.AndAlso`
  * `OR` → `Expression.OrElse`

**Example:**

```
(Age > 30 AND Salary < 5000) OR (IsActive == true)
```

---

### 3. `BuildConditionExpression<T>`

```csharp
private Expression BuildConditionExpression<T>(FilterCondition condition, ParameterExpression parameter)
```

**Purpose:**

* Converts a single filter condition into an expression

**What it does:**

* Resolves property (supports nested fields like `Customer.Name`)
* Parses operator (EQ, GT, LT, etc.)
* Builds corresponding expression

**Supported Operators:**

* `EQ`, `NEQ`
* `GT`, `LT`
* `CONTAINS`, `LIKE`
* `IN`
* `BETWEEN`

---

### 4. `BuildStringContainsExpression`

```csharp
private Expression BuildStringContainsExpression(Expression propertyExpression, ConstantExpression constantValue)
```

**Purpose:**

* Handles string search operations

**What it does:**

* Performs case-insensitive search
* Ensures property is not null
* Uses `.ToLower().Contains()`

---

### 5. `BuildInExpression`

```csharp
private Expression BuildInExpression(Expression propertyExpression, object? value, Type targetType)
```

**Purpose:**

* Handles `IN` operator

**What it does:**

* Converts input into a typed list
* Checks if property exists in the list

**Example:**

```csharp
x => new[] {1, 2, 3}.Contains(x.Id)
```

---

### 6. `BuildBetweenExpression`

```csharp
private Expression BuildBetweenExpression(MemberExpression property, object? value)
```

**Purpose:**

* Handles range filtering

**What it does:**

* Ensures exactly two values
* Builds:

```csharp
x => x.Value >= lower && x.Value <= upper
```

---

### 7. `ExtractValue`

```csharp
private object? ExtractValue(object? input, Type targetType)
```

**Purpose:**

* Converts input values (especially JSON) into correct C# types

**What it does:**

* Handles:

  * Strings
  * Numbers
  * Booleans
  * Dates
  * Arrays
* Ensures type compatibility before building expressions

---

## 🚀 Example Usage

### Sample Request

```json
{
  "operator": "AND",
  "conditions": [
    { "field": "Age", "operator": "GT", "value": 30 }
  ],
  "groups": [
    {
      "operator": "OR",
      "conditions": [
        { "field": "Salary", "operator": "LT", "value": 5000 },
        { "field": "Salary", "operator": "GT", "value": 10000 }
      ]
    }
  ]
}
```

---

### Generated Expression

```csharp
x => x.Age > 30 && (x.Salary < 5000 || x.Salary > 10000)
```

---

### Using in Repository

```csharp
var predicate = filterService.BuildPredicate<Employee>(filterGroup);

var results = dbContext.Employees
                       .Where(predicate)
                       .ToList();
```

---

## 🎯 Use Cases

* Dynamic search filters (UI-driven)
* Admin dashboards
* Reporting systems
* Generic repositories
* Advanced API filtering

---

## ⚖️ When to Use

✅ Use this when:

* Filters are dynamic (from UI/API)
* You need reusable query logic

❌ Avoid when:

* Queries are simple and fixed
* Overhead is unnecessary

---

## 🧠 Summary

* Converts **JSON filters → Expression Tree → LINQ query**
* Fully dynamic and reusable
* Supports complex nested conditions

---

## 📌 Final Note

This service is a **powerful abstraction for dynamic querying**, commonly used in scalable and enterprise-level applications.
