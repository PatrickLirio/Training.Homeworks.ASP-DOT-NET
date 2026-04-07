

using MyShop_v2.Application.Interfaces;
using MyShop_v2.Domain.Entities;
using MyShop_v2.Infrastructure.Data;
using MyShop_v2.Infrastructure.Repositories.Base;

namespace MyShop_v2.Infrastructure.Repositories
{
    public class OrderRepository : GenericRepository<Order, long>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context)
        {
            
        }
        
    }
}