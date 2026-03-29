
using System.ComponentModel.DataAnnotations;


namespace MyShop.DTO.OrderItems
{
    public class OrderItemUpdateDTO
    {
        public int ProductId { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}