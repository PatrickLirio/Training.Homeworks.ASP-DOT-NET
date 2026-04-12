using MyShop_v2.Application.DTOs.Common;

namespace MyShop_v2.Application.DTOs.Product
{
    public class ProductResponse : BaseResponse<long>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public bool IsActive { get; set; }
    }
}
