

using MyShop_v2.Domain.Entities.Base;
using MyShop_v2.Domain.Enums;

namespace MyShop_v2.Domain.Entities
{
    public class Product : AuditableEntity<long>
    {
        public string Name { get; set; }  
        public int CategoryId { get; set; } 
        public decimal Price { get; set; }
        public string? Description { get; set; } 
        public ProductStatus IsActive { get; set; } = ProductStatus.Active;
        // navigation properties

        // a product can have many items (for inventory management)
        // one-to-many relationship with Item
        public ICollection<Item> Items { get; set; } = new List<Item>();

        // Navigation
        // many-to-one relationship with Category
        // many products can belong to one category

        public Category Category { get; set; }

       public ICollection<StockMovement> StockMovements { get; set; } = new List<StockMovement>();

        
    }
}