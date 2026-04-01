
using MyShop.Configurations.Seeders;
using MyShop.Data;
using MyShop.Entities;

namespace MyShop.Seeders
{
    public class OrderItemSeeder
    {
        public static void Seed(AppDbContext context)
        {
            if (!context.OrderItems.Any())
            {
                var orderItems = new List<OrderItem>
                {
                    new OrderItem { OrderId = 1, ProductId = 1, Quantity = 2, UnitPrice = 999.99m },
                    new OrderItem { OrderId = 3, ProductId = 3, Quantity = 1, UnitPrice = 199.99m },
                    new OrderItem { OrderId = 2, ProductId = 3, Quantity = 3, UnitPrice = 199.99m }
                };

                context.OrderItems.AddRange(orderItems);
                context.SaveChanges();
            }
        }
        
    }
}