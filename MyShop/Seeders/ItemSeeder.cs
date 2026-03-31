using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyShop.Data;
using MyShop.Entities;

namespace MyShop.Seeders
{
    public class ItemSeeder
    {
        public static void Seed(AppDbContext context)
        {
            if (!context.Items.Any())
            {
                var items = new List<Item>
                {
                    new Item { ProductId = 1, StockQuantity = 100 },
                    new Item { ProductId = 2, StockQuantity = 50 },
                    new Item { ProductId = 3, StockQuantity = 200 }
                };

                context.Items.AddRange(items);
                context.SaveChanges();
            }
        }
    }
}