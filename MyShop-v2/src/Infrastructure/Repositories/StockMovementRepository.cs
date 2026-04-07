

using MyShop_v2.Application.Interfaces;
using MyShop_v2.Domain.Entities;
using MyShop_v2.Infrastructure.Data;
using MyShop_v2.Infrastructure.Repositories.Base;

namespace MyShop_v2.Infrastructure.Repositories
{
    public class StockMovementRepository : GenericRepository<StockMovement, long>, IStockMovementRepository
    {
        public StockMovementRepository (AppDbContext context) : base(context)
        {
            
        }
        
    }
}