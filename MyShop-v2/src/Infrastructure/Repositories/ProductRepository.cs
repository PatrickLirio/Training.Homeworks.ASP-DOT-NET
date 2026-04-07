
using MyShop_v2.Application.Interfaces;
using MyShop_v2.Domain.Entities;
using MyShop_v2.Infrastructure.Data;
using MyShop_v2.Infrastructure.Repositories.Base;

namespace MyShop_v2.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product, long>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
            
        }
        
    }
}