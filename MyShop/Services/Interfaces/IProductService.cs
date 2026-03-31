


using MyShop.DTO;

namespace MyShop.Services.Interfaces
{
    public interface IProductService : IStorageService
    {
        Task<IEnumerable<ProductResponseDTO>> GetProductsByCategory(string category);
    }
}