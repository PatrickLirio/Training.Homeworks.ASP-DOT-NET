
using MyShop.DTO;
using MyShop.DTO.Items;
using MyShop.DTO.Orders;


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
        Task<IEnumerable<ItemResponseDTO>> GetAllItems();
        Task<ItemResponseDTO> GetItemById(int id);
        Task AddItem(ItemCreateDTO itemInput);
        Task UpdateItem(int id, ItemUpdateDTO itemInput);
        Task DeleteItem(int id);


        

    }
}