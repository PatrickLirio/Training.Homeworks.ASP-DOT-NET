namespace MyShop_v2.Application.DTOs.Order
{
    public class OrderResponse
    {
        public long Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public List<OrderItemResponse> Items { get; set; } = new();
    }
}
