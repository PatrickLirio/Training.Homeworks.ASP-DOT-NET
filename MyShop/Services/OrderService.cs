
using MyShop.DTO.Orders;
using MyShop.Entities;
using MyShop.Mapping;
using MyShop.Repository.Interfaces;
using MyShop.Services.Interfaces;

namespace MyShop.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<OrderResponseDTO>> GetAllOrders()
        {
            var orders = await _orderRepository.GetAllAsync();
            var products = await _productRepository.GetAllAsync();
            var productLookup = products.ToDictionary(p => p.Id, p => p);
            return Helper.MapToOrderResponseDTOList(orders.ToList(), productLookup);
        }

        public async Task<IEnumerable<OrderResponseDTO>> GetOrdersByCustomer(string customerName)
        {
            var orders = await _orderRepository.GetAllAsync();
            var products = await _productRepository.GetAllAsync();
            var productLookup = products.ToDictionary(p => p.Id, p => p);
            return Helper.MapToOrderResponseDTOList(
                orders.Where(o => o.CustomerName.ToLower() == customerName.ToLower()).ToList(), productLookup);
        }
        
        public async Task<OrderResponseDTO> GetOrderById(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id)
                ?? throw new Exception($"Order with id {id} not found.");
            var products = await _productRepository.GetAllAsync();
            var productLookup = products.ToDictionary(p => p.Id, p => p);
            return Helper.MapToOrderResponseDTO(order, productLookup);
        }

        public async Task AddOrder(OrderCreateDTO orderInput)
        {
            var newId = (await _orderRepository.GetAllAsync()).Max(o => o.Id) + 1;
            var order = new Order
            {
                Id = newId,
                CustomerName = orderInput.CustomerName,
                OrderDate = DateTime.UtcNow,
                OrderItems = orderInput.Items.Select(i => new OrderItem
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            };
            await _orderRepository.AddAsync(order);
        }

        public async Task UpdateOrder(int id, OrderUpdateDTO orderInput)
        {
            var order = await _orderRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Order not found.");

            order.CustomerName = orderInput.CustomerName;
            order.OrderItems = orderInput.Items.Select(i => new OrderItem
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice
            }).ToList();

            await _orderRepository.UpdateAsync(order);
        }

        public async Task DeleteOrder(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Order not found.");
            await _orderRepository.DeleteAsync(order);
        }


    }
}