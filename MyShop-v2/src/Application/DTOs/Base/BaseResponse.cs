namespace MyShop_v2.Application.DTOs.Common
{
    public abstract class BaseResponse<TId>
    {
        public TId Id { get; set; }
    }
}
