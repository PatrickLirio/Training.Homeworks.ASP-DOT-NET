

using MyShop_v2.Domain.Entities.Base;

namespace MyShop_v2.Domain.Entities
{
    public class Item : AuditableEntity<long>
    {
        public long ProductId { get; set; }
        public int StockQuantity { get; set; }

        // navigation property
        // many-to-one relationship with Product
        // an item belongs to one product
        public Product Product { get; set; }
    }
}