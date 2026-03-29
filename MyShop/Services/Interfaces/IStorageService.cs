
using MyShop.DTO;
using MyShop.DTO.Orders;
using MyShop.Entities;

namespace MyShop.Services.Interfaces
{
    public interface IStorageService
    {
        //products
        Task<IEnumerable<ProductResponseDTO>> GetAllProducts();
        Task<ProductResponseDTO> GetProductById(int id);
        Task AddProduct(ProductCreateDTO productInput);
        Task UpdateProduct(int Id, ProductUpdateDTO productInput);
        Task DeleteProduct(int Id);


        //orders
        Task<IEnumerable<OrderResponseDTO>> GetAllOrders();
        Task<OrderResponseDTO> GetOrderById(int id);
        Task AddOrder(OrderCreateDTO orderInput);
        Task UpdateOrder(int id, OrderUpdateDTO orderInput);
        Task DeleteOrder(int id);

        // //items
        // Task<List<Item>> GetAllItems();


        

    }
}