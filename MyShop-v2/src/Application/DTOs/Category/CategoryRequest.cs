using System.ComponentModel.DataAnnotations;

namespace MyShop_v2.Application.DTOs.Category
{
    public class CategoryRequest
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public int? ParentID { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
