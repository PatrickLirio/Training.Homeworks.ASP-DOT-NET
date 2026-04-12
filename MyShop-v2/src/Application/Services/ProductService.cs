using MyShop_v2.Application.Filters;
using MyShop_v2.Application.Interfaces;
using MyShop_v2.Application.Services.Base;
using MyShop_v2.Domain.Entities;
using MyShop_v2.Application.DTOs;
using AutoMapper;
using MyShop_v2.Application.DTOs.Product;

namespace MyShop_v2.Application.Services
{
    public class ProductService : GenericService<Product, long, ProductRequest, ProductResponse>
    {
        public ProductService(IProductRepository repository, 
                              FilterService filterService, 
                              IMapper mapper) : base (repository, filterService, mapper)
        {
            
        }
    }
}