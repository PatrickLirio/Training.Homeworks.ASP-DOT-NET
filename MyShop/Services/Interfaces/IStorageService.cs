
using MyShop.DTO;
using MyShop.DTO.Orders;
using MyShop.Entities;

namespace MyShop.Services.Interfaces
{
    public interface IStorageService
    {
        Task<List<ProductResponseDTO>> GetAllProductsAsync();
        Task<List<OrderResponseDTO>> GetAllOrdersAsync();
        Task<List<Item>> GetAllItemsAsync();

    }
}