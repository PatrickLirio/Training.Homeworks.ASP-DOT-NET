
namespace MyShop.DTO.Items
{
    public class ItemResponseDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty; 
        public int StockQuantity { get; set; } 
    }
}