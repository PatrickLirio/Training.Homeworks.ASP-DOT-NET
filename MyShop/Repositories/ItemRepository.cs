

using Microsoft.EntityFrameworkCore;
using MyShop.Data;
using MyShop.Entities;
using MyShop.Repository.Interfaces;

namespace MyShop.Repository
{
    public class ItemRepository : IItemRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Item> items;
        public ItemRepository(AppDbContext context)
        {
            this._context = context;
            this.items = _context.Items;
        }

        public async Task<IEnumerable<Item>> GetAllAsync() => await items.ToListAsync();

        public async Task<Item?> GetByIdAsync(int id) => await items.FirstOrDefaultAsync(i => i.Id == id);

        public async Task<Item?> GetByProductIdAsync(int productId) => await items.FirstOrDefaultAsync(i => i.ProductId == productId);

        public async Task<Item?> GetByNameAsync(string name) => await items.Include(i => i.Product).FirstOrDefaultAsync(i => i.Product.Name.ToLower() == name.ToLower());
        

        public async Task AddAsync(Item item)
        {
            _context.Items.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Item item)
        {
            _context.Items.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Item item)
        {
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}