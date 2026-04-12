using MyShop_v2.Api.Controllers.Base;
using MyShop_v2.Application.DTOs.Category;
using MyShop_v2.Application.Services;
using MyShop_v2.Domain.Entities;

namespace MyShop_v2.Api.Controllers
{
    public class CategoryController : GenericController<Category, int, CategoryRequest, CategoryResponse>
    {
        public CategoryController(CategoryService service) : base(service)
        {
        }
    }
}