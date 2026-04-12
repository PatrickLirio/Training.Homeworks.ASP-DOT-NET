using MyShop_v2.Application.Filters;
using MyShop_v2.Application.Interfaces;
using MyShop_v2.Application.Interfaces.Base;
using MyShop_v2.Application.Services.Base;
using MyShop_v2.Domain.Entities;
using MyShop_v2.Application.DTOs;
using AutoMapper;
using MyShop_v2.Application.DTOs.Category;

namespace MyShop_v2.Application.Services
{
    public class CategoryService : GenericService<Category, int, CategoryRequest, CategoryResponse>
    {
        public CategoryService(ICategoryRpository repository, 
                               FilterService filterService, 
                               IMapper mapper) : base (repository, filterService, mapper)
        {
            
        }
        
    }
}