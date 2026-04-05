

using MyShop_v2.Domain.Entities.Base;

namespace MyShop_v2.Domain.Entities
{
    public class OrderItem : AuditableEntity<long>
    {
        public long OrderId { get; set; }
        public int ProductId { get; set; }
        public int OrderQuantity { get; set; }
        public decimal UnitPrice { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}