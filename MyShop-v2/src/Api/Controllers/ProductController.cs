using MyShop_v2.Api.Controllers.Base;
using MyShop_v2.Application.DTOs.Product;
using MyShop_v2.Application.Services;
using MyShop_v2.Domain.Entities;

namespace MyShop_v2.Api.Controllers
{
    public class ProductController : GenericController<Product, long, ProductRequest, ProductResponse>
    {
        public ProductController(ProductService service) : base(service)
        {
        }
    }
}