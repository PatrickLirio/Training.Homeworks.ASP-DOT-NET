using MyShop_v2.Application.Filters;
using MyShop_v2.Application.Interfaces;
using MyShop_v2.Application.Services.Base;
using MyShop_v2.Domain.Entities;
using MyShop_v2.Application.DTOs;
using AutoMapper;
using MyShop_v2.Application.DTOs.StockMovement;

namespace MyShop_v2.Application.Services
{
    public class StockMovementService : GenericService<StockMovement, long, StockMovementRequest, StockMovementResponse>
    {
        public StockMovementService(IStockMovementRepository repository, 
                                    FilterService filterService, 
                                    IMapper mapper) : base (repository, filterService, mapper)
        {
            
        }
        
    }
}