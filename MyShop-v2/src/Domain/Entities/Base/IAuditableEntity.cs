


namespace MyShop_v2.Domain.Entities.Base.Interface
{
    public interface IAuditableEntity : IEntity
    {
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        
    }
}