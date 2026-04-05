

using MyShop_v2.Domain.Entities.Base;
using MyShop_v2.Domain.Enums;

namespace MyShop_v2.Domain.Entities
{
    public class Order : AuditableEntity<long>
    {
        public string OrderNo { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}