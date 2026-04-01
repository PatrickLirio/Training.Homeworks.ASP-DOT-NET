

using Microsoft.EntityFrameworkCore;
using MyShop.Data;
using MyShop.Entities;
using MyShop.Repository.Interfaces;

namespace MyShop.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Order> orders;
        public OrderRepository(AppDbContext context)
        {
           this._context = context;
           this.orders = _context.Orders;
        }

        public async Task<IEnumerable<Order>> GetAllAsync() => await orders.Include(o => o.OrderItems).ThenInclude(oi => oi.Product).ToListAsync();

        public async Task<Order?> GetByIdAsync(int id) => await orders.Include(o => o.OrderItems).ThenInclude(oi => oi.Product).FirstOrDefaultAsync(o => o.Id == id);

        public async Task<IEnumerable<Order>> GetByCustomerAsync(string customerName) => await orders.Include(o => o.OrderItems).ThenInclude(oi => oi.Product).Where(o => o.CustomerName.ToLower() == customerName.ToLower()).ToListAsync();

        public async Task AddAsync(Order order)
        {
            await orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Order order)
        {
            orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Order order)
        {
            orders.Remove(order);
            await _context.SaveChangesAsync();
        }

    }
}