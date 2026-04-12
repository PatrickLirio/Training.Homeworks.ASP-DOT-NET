using System.ComponentModel.DataAnnotations;

namespace MyShop_v2.Application.DTOs.Order
{
    public class OrderItemRequest
    {
        [Required]
        public long ProductId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal UnitPrice { get; set; }
    }
}
