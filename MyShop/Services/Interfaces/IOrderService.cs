

using MyShop.DTO.Orders;

namespace MyShop.Services.Interfaces
{
    public interface IOrderService 
    {
        Task<IEnumerable<OrderResponseDTO>> GetAllOrders();
        Task<IEnumerable<OrderResponseDTO>> GetOrdersByCustomer(string customerName);
        Task<OrderResponseDTO> GetOrderById(int id);
        Task AddOrder(OrderCreateDTO orderInput);
        Task UpdateOrder(int id, OrderUpdateDTO orderInput);
        Task DeleteOrder(int id);
    }
}