
using MyShop.DTO;
using MyShop.DTO.OrderItems;
using MyShop.DTO.Orders;
using MyShop.Entities;

namespace MyShop.Mapping
{
    public class Helper
    {
        //Map through this method to convert Product to ProductResponseDTO
        public static ProductResponseDTO MapToResponseDTO(Product product) => new()
        {
            Id          = product.Id,
            Name        = product.Name,
            Price       = product.Price,
            Description = product.Description,
            Category    = product.Category
        };

        // Map a list of Products to a list of ProductResponseDTOs
        public static List<ProductResponseDTO> MapToResponseDTOList(List<Product> products)
            => products.Select(MapToResponseDTO).ToList();

        // Alternative implementation without LINQ
        internal static ProductResponseDTO MapToResponseDTO(ProductResponseDTO product)
        {
            throw new NotImplementedException();
        }

        public static OrderResponseDTO MapToOrderResponseDTO(Order order) => new()
        {
            Id           = order.Id,
            CustomerName = order.CustomerName,
            OrderDate    = order.OrderDate,
            TotalAmount  = order.TotalAmount,
            Items        = order.OrderItems.Select(i => new OrderItemResponseDTO
            {
                ProductId   = i.ProductId,
                ProductName = i.Product?.Name ?? string.Empty,
                Quantity    = i.Quantity,
                UnitPrice   = i.UnitPrice,
                Subtotal    = i.UnitPrice * i.Quantity
            }).ToList()
        };
    }
}