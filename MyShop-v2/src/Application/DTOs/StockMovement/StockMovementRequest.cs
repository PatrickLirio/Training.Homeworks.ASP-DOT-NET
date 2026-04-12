using System.ComponentModel.DataAnnotations;
using MyShop_v2.Domain.Enums;

namespace MyShop_v2.Application.DTOs.StockMovement
{
    public class StockMovementRequest
    {
        [Required]
        public long ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public StockMovementType MovementType { get; set; }
    }
}
