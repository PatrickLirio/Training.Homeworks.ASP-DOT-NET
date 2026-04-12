

using MyShop_v2.Application.Filters;
using MyShop_v2.Application.Interfaces;
using MyShop_v2.Application.Services.Base;
using MyShop_v2.Domain.Entities;

namespace MyShop_v2.Application.Services
{ 
    public class OrderItemService : GenericService<OrderItem, long>
    {
        public OrderItemService(IOrderItemRepository repository, FilterService filterService) : base(repository, filterService)
        {
            
        }
    }
}