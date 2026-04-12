using MyShop_v2.Api.Controllers.Base;
using MyShop_v2.Application.DTOs.StockMovement;
using MyShop_v2.Application.Services;
using MyShop_v2.Domain.Entities;

namespace MyShop_v2.Api.Controllers
{
    public class StockMovementController : GenericController<StockMovement, long, StockMovementRequest, StockMovementResponse>
    {
        public StockMovementController(StockMovementService service) : base(service)
        {
        }
    }
}