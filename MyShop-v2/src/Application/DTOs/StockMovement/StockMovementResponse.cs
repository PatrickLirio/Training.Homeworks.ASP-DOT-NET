namespace MyShop_v2.Application.DTOs.StockMovement
{
    public class StockMovementResponse
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public string? ProductName { get; set; } // Useful for display
        public int Quantity { get; set; }
        public string MovementType { get; set; } // Enum converted to string
        public DateTime LastUpdatedAt { get; set; }
        public string LastUpdatedBy { get; set; }
    }
}
