

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyShop_v2.Domain.Entities;
using MyShop_v2.Infrastructure.Configurations.Base;

namespace MyShop_v2.Infrastructure.Configurations
{
    public class ProductConfiguration 
                : AuditableConfiguration<Product, long>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            base.Configure(builder);
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.Description)
                .HasMaxLength(500);

            builder.Property(p => p.CategoryId)
                .IsRequired();

            builder.Property(p => p.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.IsActive)
                .IsRequired();

        //indexes
        builder.HasIndex(p => p.CategoryId);
        builder.HasIndex(p => p.IsActive );

        // relationships
            builder.HasMany(p => p.Items)
                    .WithOne(i => i.Product)
                    .HasForeignKey(i => i.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);
    
            // builder.HasOne(p => p.Category)
            //         .WithMany()
            //         .HasForeignKey(p => p.CategoryId)
            //         .OnDelete(DeleteBehavior.NoAction);

        }
        
    }
}