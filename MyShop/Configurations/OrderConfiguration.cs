

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyShop.Entities;

namespace MyShop.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(o => o.Id);

            builder.Property(o => o.CustomerName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(o => o.OrderDate)
                .IsRequired();

            // TotalAmount is a computed property, so tell EF to ignore it
            builder.Ignore(o => o.TotalAmount);
        }
    }
}