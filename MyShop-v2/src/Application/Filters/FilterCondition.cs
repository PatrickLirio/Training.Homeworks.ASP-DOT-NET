
#region --Example JSON View
/*
*
    {
        field:name,
        operator: 1,
        value: laptop
    }

*/
#endregion

namespace MyShop_v2.Application.Filters
{
    public class FilterCondition
    {
        public string Field { get; set; } = string.Empty;
        public string Operator { get; set; } = string.Empty;
        public string? Value { get; set; } 
    }
}