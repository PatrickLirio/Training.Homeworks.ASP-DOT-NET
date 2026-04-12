namespace MyShop_v2.Application.DTOs.Common
{
    public abstract class BaseAuditableResponse<TId> : BaseResponse<TId>
    {
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedAt { get; set; }
    }
}
