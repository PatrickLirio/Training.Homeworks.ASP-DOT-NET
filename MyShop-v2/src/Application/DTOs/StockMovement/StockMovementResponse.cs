using MyShop_v2.Application.DTOs.Common;

namespace MyShop_v2.Application.DTOs.StockMovement
{
    public class StockMovementResponse : BaseAuditableResponse<long>
    {
        public long ProductId { get; set; }
        public string? ProductName { get; set; } // Useful for display
        public int Quantity { get; set; }
        public string MovementType { get; set; } // Enum converted to string
    }
}
