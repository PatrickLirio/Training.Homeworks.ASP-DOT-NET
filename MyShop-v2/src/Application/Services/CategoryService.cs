

using MyShop_v2.Application.Filters;
using MyShop_v2.Application.Interfaces.Base;
using MyShop_v2.Application.Services.Base;
using MyShop_v2.Domain.Entities;

namespace MyShop_v2.Application.Services
{
    public class CategoryService : GenericService<Category, int>
    {
        public CategoryService(ICategoryRpository repository, FilterService filterService) : base(repository, filterService)
        {
            
        }
        
    }
}