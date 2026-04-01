

using System.Text.Json;
using MyShop.DTO;
using MyShop.DTO.Items;
using MyShop.DTO.Orders;
using MyShop.Entities;
using MyShop.Mapping;
using MyShop.Repository.Interfaces;
using MyShop.Services.Interfaces;

namespace MyShop.Services
{
    public class StorageService 
    {
        // private readonly IProductRepository _productRepository;
        // private readonly IItemRepository _itemRepository;
        // private readonly IOrderRepository _orderRepository;

        
        // public StorageService(IProductRepository productRepository, IItemRepository itemRepository, IOrderRepository orderRepository)
        // {
        //     _productRepository = productRepository;
        //     _itemRepository = itemRepository;
        //     _orderRepository = orderRepository;   
        // }

        //Products
        // public async Task<IEnumerable<ProductResponseDTO>> GetAllProducts()
        // {
        //     var products = await _productRepository.GetAllAsync();
        //     return Helper.MapToProductResponseDTOList(products.ToList());
        // }

        // public async Task<ProductResponseDTO> GetProductById(int id)
        // {
        //     var product = await _productRepository.GetByIdAsync(id)
        //         ?? throw new Exception($"Product with id {id} not found.");
        //     return Helper.MapToProductResponseDTO(product);
        // }

        // public async Task AddProduct(ProductCreateDTO productInput)
        // {
        //     if (await _productRepository.NameExistsAsync(productInput.Name))
        //         throw new InvalidOperationException("A product with the same name already exists.");

        //     var newId   = (await _productRepository.GetAllAsync()).Max(p => p.Id) + 1;
        //     var product = Helper.MapToProductEntity(productInput, newId);
        //     await _productRepository.AddAsync(product);
        // }

        // public async Task UpdateProduct(int Id, ProductUpdateDTO productInput)
        // {
        //     var product = await _productRepository.GetByIdAsync(Id)
        //     ?? throw new KeyNotFoundException("Product not found.");

        //     if (await _productRepository.NameExistsAsync(productInput.Name, excludeId: Id))
        //         throw new InvalidOperationException("A product with the same name already exists.");

        //     product.Name        = productInput.Name;
        //     product.Price       = productInput.Price;
        //     product.Description = productInput.Description;
        //     product.Category    = productInput.Category;

        //     await _productRepository.UpdateAsync(product);
        // }

        // public async Task DeleteProduct(int id)l
        // {
        //     var product = await _productRepository.GetByIdAsync(id)
        //         ?? throw new KeyNotFoundException("Product not found.");
        //     await _productRepository.DeleteAsync(product);
        // }


        //Orders
        // public async Task<IEnumerable<OrderResponseDTO>> GetAllOrders()
        // {
        //     var orders = await _orderRepository.GetAllAsync();
        //     var products = await _productRepository.GetAllAsync();
        //     var productLookup = products.ToDictionary(p => p.Id, p => p);
        //     return Helper.MapToOrderResponseDTOList(orders.ToList(), productLookup);
        // }

        // public async Task<OrderResponseDTO> GetOrderById(int id)
        // {
        //     var order = await _orderRepository.GetByIdAsync(id)
        //         ?? throw new Exception($"Order with id {id} not found.");
        //     var products = await _productRepository.GetAllAsync();
        //     var productLookup = products.ToDictionary(p => p.Id, p => p);
        //     return Helper.MapToOrderResponseDTO(order, productLookup);
        // }

        // public async Task AddOrder(OrderCreateDTO orderInput)
        // {
        //     var newId = (await _orderRepository.GetAllAsync()).Max(o => o.Id) + 1;
        //     var order = new Order
        //     {
        //         Id = newId,
        //         CustomerName = orderInput.CustomerName,
        //         OrderDate = DateTime.UtcNow,
        //         OrderItems = orderInput.Items.Select(i => new OrderItem
        //         {
        //             ProductId = i.ProductId,
        //             Quantity = i.Quantity,
        //             UnitPrice = i.UnitPrice
        //         }).ToList()
        //     };
        //     await _orderRepository.AddAsync(order);
        // }

        // public async Task UpdateOrder(int id, OrderUpdateDTO orderInput)
        // {
        //     var order = await _orderRepository.GetByIdAsync(id)
        //         ?? throw new KeyNotFoundException("Order not found.");

        //     order.CustomerName = orderInput.CustomerName;
        //     order.OrderItems = orderInput.Items.Select(i => new OrderItem
        //     {
        //         ProductId = i.ProductId,
        //         Quantity = i.Quantity,
        //         UnitPrice = i.UnitPrice
        //     }).ToList();

        //     await _orderRepository.UpdateAsync(order);
        // }

        // public async Task DeleteOrder(int id)
        // {
        //     var order = await _orderRepository.GetByIdAsync(id)
        //         ?? throw new KeyNotFoundException("Order not found.");
        //     await _orderRepository.DeleteAsync(order);
        // }


        // Items
        // public async Task<IEnumerable<ItemResponseDTO>> GetAllItems()
        // {
        //     var items = await _itemRepository.GetAllAsync();
        //     var products = await _productRepository.GetAllAsync();
        //     var productLookup = products.ToDictionary(p => p.Id, p => p);
        //     return items.Select(item => Helper.MapToItemResponseDTO(item, productLookup)).ToList();
        // }

        // public async Task<ItemResponseDTO> GetItemById(int id)
        // {
        //     var item = await _itemRepository.GetByIdAsync(id)
        //         ?? throw new Exception($"Item with id {id} not found.");
        //     var products = await _productRepository.GetAllAsync();
        //     var productLookup = products.ToDictionary(p => p.Id, p => p);
        //     return Helper.MapToItemResponseDTO(item, productLookup);
        // }

        // public async Task AddItem(ItemCreateDTO itemInput)
        // {
        //     var newId = (await _itemRepository.GetAllAsync()).Max(i => i.Id) + 1;
        //     var item = new Item
        //     {
        //         Id = newId,
        //         ProductId = itemInput.ProductId,
        //         StockQuantity = itemInput.Quantity
        //     };
        //     await _itemRepository.AddAsync(item);
        // }

        // public async Task UpdateItem(int id, ItemUpdateDTO itemInput)
        // {
        //     var item = await _itemRepository.GetByIdAsync(id)
        //         ?? throw new KeyNotFoundException("Item not found.");

        //     item.StockQuantity = itemInput.Quantity;

        //     await _itemRepository.UpdateAsync(item);
        // }

        // public async Task DeleteItem(int id)
        // {
        //     var item = await _itemRepository.GetByIdAsync(id)
        //         ?? throw new KeyNotFoundException("Item not found.");
        //     await _itemRepository.DeleteAsync(item);
        // }

    }
}