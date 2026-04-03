

using MyShop.Entities;

namespace MyShop.Repository.Interfaces
{
    public interface IItemRepository
    {
        Task<IEnumerable<Item>> GetAllAsync();
        Task<Item?> GetByIdAsync(int id);
        Task<Item?> GetByProductIdAsync(int productId);
        Task<Item?> GetByNameAsync(string name);
        Task AddAsync(Item item);
        Task UpdateAsync(Item item);
        Task DeleteAsync(Item item);
    }
}