

using MyShop_v2.Domain.Entities.Base;

namespace MyShop_v2.Domain.Entities
{
    public class OrderItem : AuditableEntity<long>
    {
        public long OrderId { get; set; }
        public long ProductId { get; set; }
        public int OrderQuantity { get; set; }
        public decimal UnitPrice { get; set; }

        // Navigation properties
        // many-to-one relationship with Order
        // an order item belongs to one order
        public Order Order { get; set; }
        // many-to-one relationship with Product
        // an order item belongs to one product
        public Product Product { get; set; }
    }
}