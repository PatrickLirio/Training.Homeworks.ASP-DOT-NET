using AutoMapper;
using MyShop_v2.Domain.Entities;
using MyShop_v2.Application.DTOs.Product;
using MyShop_v2.Application.DTOs.Category;
using MyShop_v2.Application.DTOs.Order;
using MyShop_v2.Application.DTOs.StockMovement;

namespace MyShop_v2.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Category Mappings
            CreateMap<Category, CategoryResponse>()
                .ForMember(dest => dest.ParentName, opt => opt.MapFrom(src => src.Parent != null ? src.Parent.Name : null));
            CreateMap<CategoryRequest, Category>();

            // Product Mappings
            CreateMap<Product, ProductResponse>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<ProductRequest, Product>();

            // Order Mappings
            CreateMap<Order, OrderResponse>();
            CreateMap<OrderRequest, Order>();
            
            // OrderItem Mappings
            CreateMap<OrderItem, OrderItemResponse>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));
            CreateMap<OrderItemRequest, OrderItem>();

            // StockMovement Mappings
            CreateMap<StockMovement, StockMovementResponse>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));
            CreateMap<StockMovementRequest, StockMovement>();
        }
    }
}
