

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyShop_v2.Domain.Entities;
using MyShop_v2.Infrastructure.Configurations.Base;

namespace MyShop_v2.Infrastructure.Configurations
{
    public class OrderItemConfiguration : AuditableConfiguration<OrderItem, long>
    {
        public override void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            base.Configure(builder);

            builder.Property(oi => oi.OrderId)
                .IsRequired();
            
            builder.Property(oi => oi.ProductId)
                .IsRequired();

            builder.Property(oi => oi.OrderQuantity)
                .IsRequired();
                

            builder.Property(oi => oi.UnitPrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

        // indexes
            builder.HasIndex(oi => oi.OrderId);
            builder.HasIndex(oi => oi.ProductId);
        // relationships
            builder.HasOne(oi => oi.Order)
                    .WithMany(o => o.OrderItems)
                    .HasForeignKey(oi => oi.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(oi => oi.Product)
                    .WithMany()
                    .HasForeignKey(oi => oi.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}