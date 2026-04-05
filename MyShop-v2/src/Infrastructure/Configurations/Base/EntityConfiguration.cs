/*
* This file Applies to all entities in the database 
* "Rule that is reusable across all entities which state that all tables should have ID"
*
*/

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyShop_v2.Domain.Entities.Base;

namespace MyShop_v2.Infrastructure.Configurations.Base
{
    public class EntityConfiguration<T, TId> : IEntityTypeConfiguration<T> where T : Entity<TId>
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}