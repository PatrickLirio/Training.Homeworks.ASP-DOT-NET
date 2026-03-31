

using Microsoft.EntityFrameworkCore;
using MyShop.Data;
using MyShop.Entities;
using MyShop.Repository.Interfaces;

namespace MyShop.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;
        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAllAsync() => await _context.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.Product).ToListAsync();

        public async Task<Order?> GetByIdAsync(int id) => await _context.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.Product).FirstOrDefaultAsync(o => o.Id == id);

        public async Task<IEnumerable<Order>> GetByCustomerAsync(string customerName) => await _context.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.Product).Where(o => o.CustomerName == customerName).ToListAsync();

        public async Task AddAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Order order)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }

    }
}