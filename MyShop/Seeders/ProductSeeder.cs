

using MyShop.Data;
using MyShop.Entities;

namespace MyShop.Configurations.Seeders
{
    public class ProductSeeder
    {
        public static void Seed(AppDbContext context)
        {
            if (!context.Products.Any())
            {
                var products = new List<Product>
                {
                    new Product { Name = "Laptop", Description = "A high-performance laptop", Category = "Electronics", Price = 999.99m },
                    new Product { Name = "Smartphone", Description = "A latest model smartphone", Category = "Electronics", Price = 699.99m },
                    new Product { Name = "Headphones", Description = "Noise-cancelling headphones", Category = "Electronics", Price = 199.99m },
                    new Product { Name = "Coffee Maker", Description = "A programmable coffee maker", Category = "Home Appliances", Price = 49.99m },
                    new Product { Name = "Blender", Description = "A powerful blender for smoothies", Category = "Home Appliances", Price = 89.99m }
                };

                context.Products.AddRange(products);
                context.SaveChanges();
            }
        }
        
    }
}