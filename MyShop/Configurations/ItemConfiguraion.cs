

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyShop.Entities;

namespace MyShop.Configurations
{
    public class ItemConfiguraion : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.ToTable("Items");
            builder.HasKey(i => i.Id);

            // can't be negative
            builder.Property(i => i.StockQuantity)
                .IsRequired();


            // Configure the relationship with Product
            // "An Item has ONE Product, and a Product has MANY Items."
            builder.HasOne(i => i.Product)
                .WithMany(p => p.Items)
                .HasForeignKey(i => i.ProductId)
                .OnDelete(DeleteBehavior.Cascade); // If you delete a product, its inventory items are deleted too
                }
    }
}