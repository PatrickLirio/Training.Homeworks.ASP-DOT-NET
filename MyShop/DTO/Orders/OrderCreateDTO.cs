

using System.ComponentModel.DataAnnotations;
using MyShop.DTO.OrderItems;

namespace MyShop.DTO.Orders
{
    public class OrderCreateDTO
    {
         [Required]
        public string CustomerName { get; set; } = string.Empty;

        [MinLength(1, ErrorMessage = "Order must contain at least one item")]
        public List<OrderItemCreateDTO> Items { get; set; } = new();


    }
}