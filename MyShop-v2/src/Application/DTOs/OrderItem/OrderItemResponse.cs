using MyShop_v2.Application.DTOs.Common;

namespace MyShop_v2.Application.DTOs.Order
{
    public class OrderItemResponse : BaseResponse<long>
    {
        public long ProductId { get; set; }
        public string? ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice => Quantity * UnitPrice;
    }
}
