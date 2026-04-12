using MyShop_v2.Api.Controllers.Base;
using MyShop_v2.Application.DTOs.Order;
using MyShop_v2.Application.Services;
using MyShop_v2.Domain.Entities;

namespace MyShop_v2.Api.Controllers
{
    public class OrderControllers : GenericController<Order, long, OrderRequest, OrderResponse>
    {
        public OrderControllers(OrderService service) : base(service)
        {
        }
    }
}