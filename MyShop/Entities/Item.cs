

namespace MyShop.Entities
{
    public class Item
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int StockQuantity { get; set; }
        public Product Product { get; set; } = null!;
        
    }
}