using MyShop_v2.Application.DTOs.Common;

namespace MyShop_v2.Application.DTOs.Category
{
    public class CategoryResponse : BaseResponse<int>
    {
        public string Name { get; set; }
        public int? ParentID { get; set; }
        public bool IsActive { get; set; }
        public string? ParentName { get; set; }
    }
}
