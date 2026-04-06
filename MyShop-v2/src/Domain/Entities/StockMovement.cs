/*
* In this class we will define the StockMovement entity which will represent the movement of stock in our inventory system.
* This will include properties such as the quantity of stock moved, the type of movement (e.g. incoming, outgoing), and the date of the movement.
* basically the history of the stock movement for each product in our inventory system.
* This will serve as a tracker.
*/
using MyShop_v2.Domain.Entities.Base;
using MyShop_v2.Domain.Enums;

namespace MyShop_v2.Domain.Entities
{
    public class StockMovement : AuditableEntity<long>
    {
        public long ProductId { get; set; }
        public int Quantity { get; set; }
        public StockMovementType MovementType { get; set; } = StockMovementType.OrderPlaced; // default to OrderPlaced, but can be set to other types (e.g. StockAdjustment, Purchase, etc.)
  
        // Navigation property
        // many-to-one relationship with Product
        // a stock movement belongs to one product
        public Product Product { get; set; }
        
    }
}