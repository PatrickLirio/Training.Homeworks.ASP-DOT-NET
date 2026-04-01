

using MyShop.Entities;

namespace MyShop.Repository.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task<Product?> GetByNameAsync(string name);
        Task<bool> NameExistsAsync(string name, int? excludeId = null);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Product product);
    }
}