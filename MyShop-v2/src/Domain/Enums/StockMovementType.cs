namespace MyShop_v2.Domain.Enums
{
    public enum StockMovementType
    {
        OrderPlaced = 0,   // when an order is placed, stock is reduced
        OrderCancelled = 1, // when an order is cancelled, stock is increased
        StockAdjustment = 2, // manual adjustment of stock (e.g. inventory count)
        StockTransfer = 3,   // transfer of stock between locations (if we have multiple warehouses)
        Purchase = 4, // when we purchase new stock from suppliers, stock is increased
        ReturnToSupplier = 5 // when we return stock to suppliers, stock is reduced
        
    }
}