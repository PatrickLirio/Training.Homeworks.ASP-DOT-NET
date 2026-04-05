/* this will create an ID and Auditable entity (LastUpdatedBy, LastUpdatedAt) 
 * that all entities can inherit from
*/

using MyShop_v2.Domain.Entities.Base.Interface;

namespace MyShop_v2.Domain.Entities.Base
{
    public abstract class AuditableEntity <TId> : Entity<TId>, IAuditableEntity
    {
        public string LastUpdatedBy { get; set; } = "System";
        public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;
    }
}