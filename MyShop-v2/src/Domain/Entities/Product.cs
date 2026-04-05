

using MyShop_v2.Domain.Entities.Base;
using MyShop_v2.Domain.Enums;

namespace MyShop_v2.Domain.Entities
{
    public class Product : AuditableEntity<long>
    {
        public string Name { get; set; }  
        public int CategoryID { get; set; } 
        public decimal Price { get; set; }
        public string? Description { get; set; } 
        public ProductStatus IsActive { get; set; } = ProductStatus.Active;
        // navigation properties

        public List<Item> Items { get; set; } = new List<Item>();
        
    }
}