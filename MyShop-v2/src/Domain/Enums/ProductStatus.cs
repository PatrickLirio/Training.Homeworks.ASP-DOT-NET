namespace MyShop_v2.Domain.Enums
{
    public enum ProductStatus
    {
        Draft = 0, // Not yet available for sale, still being prepared or reviewed
        Active = 1,
        OutOfStock = 2, // temporarily not available
        Discontinued = 3,  
        Archived = 4
    }
}