


using MyShop.DTO;

namespace MyShop.Services.Interfaces
{
    public interface IProductService
    {
        
        Task<IEnumerable<ProductResponseDTO>> GetAllProducts();
        Task<ProductResponseDTO> GetProductById(int id);
        Task<IEnumerable<ProductResponseDTO>> GetProductsByCategory(string category);
        Task AddProduct(ProductCreateDTO productInput);
        Task UpdateProduct(int Id, ProductUpdateDTO productInput);
        Task DeleteProduct(int Id);
    }
}