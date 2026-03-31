

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyShop.Entities;

namespace MyShop.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");
            builder.HasKey(oi => oi.Id);

            builder.Property(oi => oi.UnitPrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(oi => oi.Quantity)
                .IsRequired();

            //relationship to order -> orderItems (one to many) 
            builder.HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade); // Delete items if order is deleted


            //relationship to product -> orderItems (one to many)
            builder.HasOne(oi => oi.Product)
                .WithMany() // A product doesn't necessarily need a list of every order it was ever in
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict); // Don't delete order items if product is deleted, but prevent deletion if there are order items referencing it

        }
    }
}