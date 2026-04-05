// IEntity = marker for database entities
// IEntity<TId> = entity with an ID of type TId (e.g., int, long, Guid)
// TId = flexible ID type so different entities can use different key types

namespace MyShop_v2.Domain.Entities.Base.Interface
{
    public interface IEntity
    {
        
    }
    public interface IEntity<TId> : IEntity 
    {
        public TId Id { get; set; }
    }

}