using MyShop.DTO;


namespace MyShop.Services.Interfaces
{
    public interface IProductService 
    {
        Task<ProductResponseDTO> GetProductByIdAsync(int id);
        Task AddProductAsync(ProductCreateDTO productInput);
        Task UpdateProductAsync(ProductUpdateDTO productInput);
        Task DeleteProductAsync(int id);
    }
}