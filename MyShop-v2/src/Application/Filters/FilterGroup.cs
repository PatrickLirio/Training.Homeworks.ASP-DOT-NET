#region --Example JSON View
/*
*
    {
        Operator: "AND",
        Conditions:[
            {
                field:name,
                operator: 1,
                value: laptop
            }
        ],
        Groups:[
            Operator: "OR",
            Conditions:[
                {
                    field:name,
                    operator: 1,
                    value: laptop
                },
                 {
                    field:name,
                    operator: 1,
                    value: AMD
                },
                field:name,
                operator: 64,
                value: [
                    mouse,
                    mouse pad,
                    keyboard
                ]

            ],
        ]
    }
*
*
*/

#endregion

namespace MyShop_v2.Application.Filters
{
    public class FilterGroup
    {
        public string Operator { get; set; } = "AND"; //default: "AND" or "OR"
        public List<FilterCondition> Conditions { get; set; } = new List<FilterCondition>();
        public List<FilterGroup> Groups { get; set; } = new List<FilterGroup>();
    }
}