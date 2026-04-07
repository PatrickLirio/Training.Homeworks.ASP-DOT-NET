

using MyShop_v2.Application.Interfaces;
using MyShop_v2.Domain.Entities;
using MyShop_v2.Infrastructure.Data;
using MyShop_v2.Infrastructure.Repositories.Base;

namespace MyShop_v2.Infrastructure.Repositories
{
    public class OrderItemRepository : GenericRepository<OrderItem, long>, IOrderItemRepository
    {
        public OrderItemRepository(AppDbContext context) : base(context)
        {
            
        }
        
    }
}