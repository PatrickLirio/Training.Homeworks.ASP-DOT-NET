
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
        Task UpdateProduct(int id, ProductUpdateDTO productInput);
        Task DeleteProduct(int id);


        //orders
        // Task<List<OrderResponseDTO>> GetAllOrders();
        // Task<OrderResponseDTO> GetOrderById(int id);
        // Task AddOrder(ProductCreateDTO OrderInput);
        // Task UpdateOrder(ProductUpdateDTO OrderInput);
        // Task DeleteOrder(int id);

        // //items
        // Task<List<Item>> GetAllItems();


        

    }
}