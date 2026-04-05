/*
* This file Applies only to auditable entities in the database 
* "Rule that is reusable across all entities which state that all tables 
*   should have LastUpdatedBy and LastUpdatedAt columns"
*
*/


using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyShop_v2.Domain.Entities.Base;

namespace MyShop_v2.Infrastructure.Configurations.Base
{
    public class AuditableConfiguration<T, TId> 
                : EntityConfiguration<T, TId>
                where T : AuditableEntity<TId>
    {
        public override void Configure(EntityTypeBuilder<T> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.LastUpdatedBy)
            .HasMaxLength(20)
            .IsRequired();
            builder.Property(x => x.LastUpdatedAt)
            .IsRequired();
        }
    }
}