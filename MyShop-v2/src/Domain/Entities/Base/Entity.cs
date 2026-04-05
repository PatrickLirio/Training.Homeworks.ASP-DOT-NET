

using MyShop_v2.Domain.Entities.Base.Interface;

namespace MyShop_v2.Domain.Entities.Base
{
    public abstract class Entity<TId> : IEntity<TId>
    {
        public TId Id { get; set; }
    }
}