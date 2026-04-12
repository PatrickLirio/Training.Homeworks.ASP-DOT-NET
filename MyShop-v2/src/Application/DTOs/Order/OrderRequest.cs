using System.ComponentModel.DataAnnotations;

namespace MyShop_v2.Application.DTOs.Order
{
    public class OrderRequest
    {
        [Required]
        public string CustomerName { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Order must have at least one item.")]
        public List<OrderItemRequest> Items { get; set; } = new();
    }
}
