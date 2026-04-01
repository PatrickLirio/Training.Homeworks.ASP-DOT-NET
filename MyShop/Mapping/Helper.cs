
using MyShop.DTO;
using MyShop.DTO.Items;
using MyShop.DTO.OrderItems;
using MyShop.DTO.Orders;
using MyShop.Entities;

namespace MyShop.Mapping
{
    public class Helper
    {


        // In Helper.cs - add this method
        public static Product MapToProductEntity(ProductCreateDTO productInput) => new()
        {
            Name        = productInput.Name,
            Price       = productInput.Price,
            Description = productInput.Description,
            Category    = productInput.Category
        };
        // Map a Product to a ProductResponseDTO
        public static ProductResponseDTO MapToProductResponseDTO(Product product) => new()
        {
            Id          = product.Id,
            Name        = product.Name,
            Description = product.Description,
            Category    = product.Category,
            Price       = product.Price,
            StockQuantity = product.Items.FirstOrDefault()?.StockQuantity ?? 0
        };

        // Map a list of Products to a list of ProductResponseDTOs
        public static List<ProductResponseDTO> MapToProductResponseDTOList(List<Product> products)
            => products.Select(MapToProductResponseDTO).ToList();

        // Map an Order to an OrderResponseDTO, including product names for each order item
        public static OrderResponseDTO MapToOrderResponseDTO(Order order, Dictionary<int, Product> productLookup) => new()
        {
            Id           = order.Id,
            CustomerName = order.CustomerName,
            OrderDate    = order.OrderDate,
            TotalAmount  = order.TotalAmount,
            Items        = order.OrderItems.Select(i => new OrderItemResponseDTO
            {
                ProductId   = i.ProductId,
                ProductName = productLookup.TryGetValue(i.ProductId, out var prod)
                            ? prod.Name
                            : "Unknown",
                Quantity    = i.Quantity,
                UnitPrice   = i.UnitPrice,
                Subtotal    = i.UnitPrice * i.Quantity
            }).ToList()
        };

        // Map a list of Orders to a list of OrderResponseDTO, using the provided product lookup for product names
        public static List<OrderResponseDTO> MapToOrderResponseDTOList(List<Order> orders, Dictionary<int, Product> productLookup)
                => orders.Select(order => MapToOrderResponseDTO(order, productLookup)).ToList();

        // Map an Item to an ItemResponseDTO, including the product name using the provided product lookup
        public static ItemResponseDTO MapToItemResponseDTO(Item item, Dictionary<int, Product> productLookup) => new()
        {
            Id          = item.Id,
            ProductId   = item.ProductId,
            ProductName = productLookup.TryGetValue(item.ProductId, out var prod)
                            ? prod.Name
                            : "Unknown",
            StockQuantity    = item.StockQuantity
        };

        // Map a list of Items to a list of ItemResponseDTO, using the provided product lookup for product names
        public static List<ItemResponseDTO> MapToItemResponseDTOList(List<Item> items, Dictionary<int, Product> productLookup)
            => items.Select(item => MapToItemResponseDTO(item, productLookup)).ToList();


    }
}