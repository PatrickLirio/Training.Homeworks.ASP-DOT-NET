

using MyShop.Data;
using MyShop.Entities;

namespace MyShop.Seeders
{
    public class OrderSeeder
    {
        
        public static void Seed(AppDbContext context)
        {
            if (!context.Orders.Any())
            {
                var orders = new List<Order>
                {
                    new Order { CustomerName = "John Doe", OrderDate = DateTime.Now.AddDays(-10) },
                    new Order { CustomerName = "Jane Smith", OrderDate = DateTime.Now.AddDays(-5) },
                    new Order { CustomerName = "Bob Johnson", OrderDate = DateTime.Now }
                };

                context.Orders.AddRange(orders);
                context.SaveChanges();
            }
        }
    }
}