

using System.ComponentModel.DataAnnotations;

namespace MyShop.DTO
{
    public class ProductCreateDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        
        [Range(0,double.MaxValue, ErrorMessage = "Price must be a non-negative value.")]
        public decimal Price { get; set; }

        public string Description { get; set; } = string.Empty;
        
        [Required]
        public string Category { get; set; } = string.Empty;
    }
}