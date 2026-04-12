using MyShop_v2.Application.DTOs.Common;

namespace MyShop_v2.Application.DTOs.Order
{
    public class OrderResponse : BaseAuditableResponse<long>
    {
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public List<OrderItemResponse> Items { get; set; } = new();
    }
}
