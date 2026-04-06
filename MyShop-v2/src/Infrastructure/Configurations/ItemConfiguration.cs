


using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyShop_v2.Domain.Entities;
using MyShop_v2.Infrastructure.Configurations.Base;

namespace MyShop_v2.Infrastructure.Configurations
{
    public class ItemConfiguration : AuditableConfiguration<Item, long>
    {
        public override void Configure(EntityTypeBuilder<Item> builder)
        {
            base.Configure(builder);

            builder.Property(i => i.ProductId)
                .IsRequired();
            
            builder.Property(i => i.StockQuantity)
                .IsRequired();

            //Indexes
            builder.HasIndex(i => i.ProductId);

            //Relationships
            builder.HasOne(i => i.Product)
                .WithMany(p => p.Items)
                .HasForeignKey(i => i.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
                
        }
        
    }
}