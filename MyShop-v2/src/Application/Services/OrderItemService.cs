using MyShop_v2.Application.Filters;
using MyShop_v2.Application.Interfaces;
using MyShop_v2.Application.Services.Base;
using MyShop_v2.Domain.Entities;
using MyShop_v2.Application.DTOs; // Assuming DTOs are in this namespace
using AutoMapper;
using MyShop_v2.Application.DTOs.Order;

namespace MyShop_v2.Application.Services
{ 
    public class OrderItemService : GenericService<OrderItem, long, OrderItemRequest, OrderItemResponse>
    {
        public OrderItemService(IOrderItemRepository repository, 
                                FilterService filterService, 
                                IMapper mapper) : base(repository, filterService, mapper)
        {
            
        }
    }
}