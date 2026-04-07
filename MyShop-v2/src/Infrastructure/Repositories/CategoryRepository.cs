
using MyShop_v2.Application.Interfaces.Base;
using MyShop_v2.Domain.Entities;
using MyShop_v2.Infrastructure.Data;
using MyShop_v2.Infrastructure.Repositories.Base;

namespace MyShop_v2.Infrastructure.Repositories
{
    public class CategoryRepository : GenericRepository<Category, int>, ICategoryRpository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
            
        }
        
    }
}