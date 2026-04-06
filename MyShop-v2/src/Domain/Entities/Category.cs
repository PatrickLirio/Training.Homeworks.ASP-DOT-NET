
using MyShop_v2.Domain.Entities.Base;

namespace MyShop_v2.Domain.Entities
{
    public class Category : Entity<int>
    {
        public string Name { get; set; }
        public int? ParentID { get; set; }
        public bool IsActive { get; set; } = true;

    // Navigation for hierarchy only
    // many-to-one relationship with itself for hierarchy
    // many child categories can belong to one parent category
    public Category Parent { get; set; }

    // one-to-many relationship with itself for hierarchy
    // one category can have many child categories
    public ICollection<Category> Children { get; set; } = new List<Category>();

    // Navigation for products
    // one-to-many relationship with Product
    // one category can have many products
    public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}