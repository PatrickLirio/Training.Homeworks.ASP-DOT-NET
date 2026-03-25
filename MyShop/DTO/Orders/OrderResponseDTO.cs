
using MyShop.DTO.OrderItems;

namespace MyShop.DTO.Orders
{
    public class OrderResponseDTO
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public  DateTime OrderDate   { get; set; }
        public decimal TotalAmount { get; set; }
        

        public List<OrderItemResponseDTO> Items { get; set; } = new();
    }
}