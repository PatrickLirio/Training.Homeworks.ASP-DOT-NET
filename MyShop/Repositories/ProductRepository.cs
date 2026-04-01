
using Microsoft.EntityFrameworkCore;
using MyShop.Data;
using MyShop.Entities;
using MyShop.Repository.Interfaces;

namespace MyShop.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Product> products;
        public ProductRepository(AppDbContext context)
        {
            // _context = context;
            this._context = context;
            products = _context.Products;
        }

        public async Task<IEnumerable<Product>> GetAllAsync() => await products.Include(p => p.Items).ToListAsync();

        public async Task<Product?> GetByIdAsync(int id) => await products.Include(p => p.Items).FirstOrDefaultAsync(p => p.Id == id);

        public async Task<Product?> GetByNameAsync(string name) => await products.Include(p => p.Items).FirstOrDefaultAsync(p => p.Name.ToLower() == name.ToLower());

        public async Task<bool> NameExistsAsync(string name, int? excludeId = null) => await products.AnyAsync(p => p.Name == name && (!excludeId.HasValue || p.Id != excludeId.Value));
       
        public async Task AddAsync(Product product) 
        {
            await products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product) 
        {
            products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Product product) 
        {
            products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}