using System.ComponentModel.DataAnnotations;

namespace MyShop_v2.Application.DTOs.Product
{
    public class ProductRequest
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }
        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
