

using MyShop_v2.Application.Interfaces.Base;
using MyShop_v2.Domain.Entities;

namespace MyShop_v2.Application.Interfaces
{
    public interface IOrderItemRepository : IRepository<OrderItem, long>
    {
        
    }
}