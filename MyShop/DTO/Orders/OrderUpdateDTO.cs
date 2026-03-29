using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MyShop.DTO.OrderItems;

namespace MyShop.DTO.Orders
{
    public class OrderUpdateDTO
    {
        [Required]
        public int Id { get; set; } 

        [Required]
        public string CustomerName { get; set; } = string.Empty;

        [MinLength(1, ErrorMessage = "Order must contain at least one item")]
        public List<OrderItemUpdateDTO> Items { get; set; } = new();
    }
}