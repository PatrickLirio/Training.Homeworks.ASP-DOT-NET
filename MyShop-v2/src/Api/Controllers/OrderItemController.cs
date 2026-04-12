using MyShop_v2.Api.Controllers.Base;
using MyShop_v2.Application.DTOs.Order;
using MyShop_v2.Application.Services;
using MyShop_v2.Domain.Entities;

namespace MyShop_v2.Api.Controllers
{
    public class OrderItemController : GenericController<OrderItem, long, OrderItemRequest, OrderItemResponse>
    {
        public OrderItemController(OrderItemService service) : base(service)
        {
        }
    }
}