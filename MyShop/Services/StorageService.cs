

using System.Text.Json;
using MyShop.DTO;
using MyShop.DTO.Orders;
using MyShop.Entities;
using MyShop.Mapping;
using MyShop.Services.Interfaces;

namespace MyShop.Services
{
    public class StorageService : IStorageService
    {
        private readonly string _productsFilePath = "products.json";
        private readonly string _ordersFilePath = "orders.json";
        private readonly string _itemsFilePath = "items.json";

//products
        public async Task<IEnumerable<ProductResponseDTO>> GetAllProducts() // used IEnumerable for read only/ immutable list
        {
            if (!File.Exists(_productsFilePath))
            {
                return Enumerable.Empty<ProductResponseDTO>();
            }
            try
            {                
            var productsJson = await File.ReadAllTextAsync(_productsFilePath);
            var products = JsonSerializer.Deserialize<List<Product>>(productsJson) ?? new List<Product>();
            return Helper.MapToProductResponseDTOList(products);
            }
            catch (JsonException)
            {
                // JSON is corrupted 
                return new List<ProductResponseDTO>();
            }
            catch (IOException)
            {
                // File access issues
                throw; 
            }
        }

        public async Task<ProductResponseDTO> GetProductById(int id)
        {
            var products = await GetAllProducts();
            var product = products.FirstOrDefault(prod => prod.Id == id) ?? throw new KeyNotFoundException($"Product with ID {id} not found.");
            return Helper.MapToProductResponseDTO(product);
        }

        public async Task AddProduct(ProductCreateDTO productInput)
        {
            var productsJson = await File.ReadAllTextAsync(_productsFilePath);
            var products = JsonSerializer.Deserialize<List<Product>>(productsJson) ?? new List<Product>();

             // Check if the name already exists (case-insensitive)
            if (products.Any(prod => prod.Name.Equals(productInput.Name, 
                StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException(
                    "A product with the same name already exists.");

            // Map DTO in Entity
            var product = new Product
            {
                Id          = products.Count > 0 ? products.Max(prod => prod.Id) + 1 : 1,
                Name        = productInput.Name,
                Price       = productInput.Price,
                Description = productInput.Description,
                Category    = productInput.Category
            };

             try
            {
                products.Add(product);
                var serializedProducts = JsonSerializer.Serialize(products);
                await File.WriteAllTextAsync(_productsFilePath, serializedProducts);
            }
            catch (IOException)
            {
                // Handle file access issues
                throw;
            } 
        }

        public async Task UpdateProduct(int Id, ProductUpdateDTO productInput)
        {
            var products = (await GetAllProducts()).ToList();
            var existingProduct = products.FirstOrDefault(prod => prod.Id == Id)
             ?? throw new KeyNotFoundException("Product not found.");

             if (products.Any(prod => prod.Id != Id && 
                prod.Name.Equals(productInput.Name, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException(
                    "A product with the same name already exists.");
            try
            {
                existingProduct.Name        = productInput.Name;
                existingProduct.Price       = productInput.Price;
                existingProduct.Description = productInput.Description;
                existingProduct.Category    = productInput.Category;
                //saving
                 await File.WriteAllTextAsync(_productsFilePath, JsonSerializer.Serialize(products));
            }
            catch (IOException) { throw; }
        }
        
        public async Task DeleteProduct(int Id)
        {
            var products = (await GetAllProducts()).ToList();
            var productToRemove = products.FirstOrDefault(prod => prod.Id == Id)
            ?? throw new KeyNotFoundException("Product not found.");

            try
            {
                products.Remove(productToRemove);
                var serializedProducts = JsonSerializer.Serialize(products);
                await File.WriteAllTextAsync(_productsFilePath, serializedProducts);
            }
            catch (IOException)
            {
                // Handle file access issues
                throw;
            } 
        }

//orders
        public async Task <IEnumerable<OrderResponseDTO>> GetAllOrders()
        {
            if (!File.Exists(_ordersFilePath))
            {
                return Enumerable.Empty<OrderResponseDTO>();
            }
            try
            {                
            var ordersJson = await File.ReadAllTextAsync(_ordersFilePath);
            var orders = JsonSerializer.Deserialize<List<Order>>(ordersJson) ?? new List<Order>();
            return Helper.MapToOrderResponseDTOList(orders);
            }
            catch (JsonException)
            {
                // JSON is corrupted 
                return new List<OrderResponseDTO>();
            }
            catch (IOException)
            {
                // File access issues
                throw; 
            }
        }

        public async Task <OrderResponseDTO> GetOrderById(int id)
        {
             var orders = await GetAllOrders();
            var order = orders.FirstOrDefault(order => order.Id == id) ?? throw new KeyNotFoundException($"Order with ID {id} not found.");
            return Helper.MapToOrderResponseDTOList(order);
        }

        public async Task AddOrder(OrderCreateDTO orderInput)
        {
            var ordersJson = await File.ReadAllTextAsync(_ordersFilePath);
            var orders = JsonSerializer.Deserialize<List<Order>>(ordersJson) ?? new List<Order>();

            // Map DTO in Entity
            var newOrder = new Order
            {
                Id          = orders.Count > 0 ? orders.Max(order => order.Id) + 1 : 1,
                CustomerName        = orderInput.CustomerName,
                OrderDate = DateTime.Now,
                OrderItems = orderInput.Items.Select(item => new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                }).ToList()
            };

             try
            {
                orders.Add(newOrder);
                var serializedOrders = JsonSerializer.Serialize(orders, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(_ordersFilePath, serializedOrders);
            }
            catch (IOException)
            {
                throw;
            }




        }
    
        public async Task UpdateOrder(int id, OrderUpdateDTO orderInput)
        {
            //  var orders = (await GetAllOrders()).ToList();
            var ordersJson = await File.ReadAllTextAsync(_ordersFilePath);
            var orders = JsonSerializer.Deserialize<List<Order>>(ordersJson) ?? new List<Order>();
            var existingOrder = orders.FirstOrDefault(order => order.Id == id)
             ?? throw new KeyNotFoundException("Order not found.");

            existingOrder.CustomerName = orderInput.CustomerName;

           existingOrder.OrderItems = orderInput.Items.Select(item => new OrderItem
            {
                OrderId = id, 
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice
            }).ToList();   
            try
            {
                await File.WriteAllTextAsync(_ordersFilePath, JsonSerializer.Serialize(orders, new JsonSerializerOptions { WriteIndented = true }));
            }
            catch (IOException)
            {
                throw;
            }
        }
    
        public async Task DeleteOrder(int id)
        {
            var orders = (await GetAllOrders()).ToList();
            var orderToRemove = orders.FirstOrDefault(order => order.Id == id)
            ?? throw new KeyNotFoundException("Order not found.");

            try
            {
                orders.Remove(orderToRemove);
                var serializedOrders = JsonSerializer.Serialize(orders, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(_ordersFilePath, serializedOrders);
            }
            catch (IOException)
            {
                throw;
            } 
        }
    
    }
}