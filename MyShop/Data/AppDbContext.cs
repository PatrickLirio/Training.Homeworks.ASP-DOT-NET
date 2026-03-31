
using Microsoft.EntityFrameworkCore;
using MyShop.Entities;

namespace MyShop.Data
{
    public class AppDbContext : DbContext
    {

        public DbSet<Product> Products { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Item> Items { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        //this is to seed the database with some initial data
        //calling the configuration method of the model builder to seed the database with some initial data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
        
    }
}