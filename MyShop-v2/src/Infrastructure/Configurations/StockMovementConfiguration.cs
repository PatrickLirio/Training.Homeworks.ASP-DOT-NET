


using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyShop_v2.Domain.Entities;
using MyShop_v2.Infrastructure.Configurations.Base;

namespace MyShop_v2.Infrastructure.Configurations
{
    public class StockMovementConfiguration : AuditableConfiguration<StockMovement, long>
    {
        public override void Configure(EntityTypeBuilder<StockMovement> builder)
        {
            base.Configure(builder);

            builder.Property(sm => sm.ProductId)
                .IsRequired();

            builder.Property(sm => sm.Quantity)
                .IsRequired();
            
            builder.Property(sm => sm.MovementType)
                .IsRequired();
            
            //Indexes
            builder.HasIndex(sm => sm.ProductId);

            //Relationship
            builder.HasOne(sm => sm.Product)
                .WithMany(sm => sm.StockMovements)
                .HasForeignKey(sm => sm.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
        
    }
}