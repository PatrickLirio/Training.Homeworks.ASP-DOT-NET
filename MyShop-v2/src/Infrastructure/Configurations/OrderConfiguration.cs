
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyShop_v2.Domain.Entities;
using MyShop_v2.Infrastructure.Configurations.Base;

namespace MyShop_v2.Infrastructure.Configurations
{
    public class OrderConfiguration : AuditableConfiguration<Order, long>
    {
        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.OrderNo)
                .IsRequired();

            builder.Property(o => o.CustomerName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(o => o.TotalAmount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(o => o.Status)
                .IsRequired();

        //indexes
            builder.HasIndex(o => o.OrderNo)
                .IsUnique();
        // relationships
            builder.HasMany(o => o.OrderItems)
                    .WithOne(oi => oi.Order)
                    .HasForeignKey(oi => oi.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);

        }
    }
}