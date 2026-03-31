

using MyShop.DTO.Orders;

namespace MyShop.Services.Interfaces
{
    public interface IOrderService : IStorageService
    {
        Task<IEnumerable<OrderResponseDTO>> GetOrdersByCustomer(string customerName);
    }
}