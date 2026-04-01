
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

        private readonly IItemRepository _itemRepository;

        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository, IItemRepository itemRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _itemRepository = itemRepository;
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
             var orderItems = new List<OrderItem>();

            foreach (var item in orderInput.Items)
            {
               
                var product = await _productRepository.GetByIdAsync(item.ProductId)
                    ?? throw new KeyNotFoundException(
                        $"Product with ID {item.ProductId} not found.");

              
                var stock = await _itemRepository.GetByProductIdAsync(item.ProductId)
                    ?? throw new InvalidOperationException(
                        $"No inventory found for ProductId {item.ProductId}");

                if (stock.StockQuantity < item.Quantity)
                    throw new InvalidOperationException(
                        $"Not enough stock for ProductId {item.ProductId}. " +
                        $"Available: {stock.StockQuantity}, Requested: {item.Quantity}");

                stock.StockQuantity -= item.Quantity;
                await _itemRepository.UpdateAsync(stock);

                orderItems.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity  = item.Quantity,
                    UnitPrice = product.Price 
                });
            }

            var order = new Order
            {
                CustomerName = orderInput.CustomerName,
                OrderDate    = DateTime.UtcNow,
                OrderItems   = orderItems
            };

            await _orderRepository.AddAsync(order);
        }

        public async Task UpdateOrder(int id, OrderUpdateDTO orderInput)
        {
            var order = await _orderRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Order not found.");

            var orderItems = new List<OrderItem>();

            foreach (var item in orderInput.Items)
            {

                var product = await _productRepository.GetByIdAsync(item.ProductId)
                    ?? throw new KeyNotFoundException(
                        $"Product with ID {item.ProductId} not found.");

                orderItems.Add(new OrderItem
                {
                    OrderId   = id,
                    ProductId = item.ProductId,
                    Quantity  = item.Quantity,
                    UnitPrice = product.Price 
                });
            }

            order.CustomerName = orderInput.CustomerName;
            order.OrderItems   = orderItems;

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