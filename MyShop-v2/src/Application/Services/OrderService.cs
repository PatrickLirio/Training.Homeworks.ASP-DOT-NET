using MyShop_v2.Application.Filters;
using MyShop_v2.Application.Interfaces;
using MyShop_v2.Application.Services.Base;
using MyShop_v2.Domain.Entities;
using MyShop_v2.Application.DTOs;
using AutoMapper;
using MyShop_v2.Application.DTOs.Order;

namespace MyShop_v2.Application.Services
{
    public class OrderService : GenericService<Order, long, OrderRequest, OrderResponse>
    {
        public OrderService (IOrderRepository repository, 
                             FilterService filterService, 
                             IMapper mapper) : base (repository, filterService, mapper)
        {
            
        }
        
    }
}