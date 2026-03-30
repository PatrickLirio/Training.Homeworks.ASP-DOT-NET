

using System.Text.Json;
using MyShop.DTO;
using MyShop.DTO.Items;
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
            var productsJson = File.Exists(_productsFilePath) ? await File.ReadAllTextAsync(_productsFilePath) : "[]";

            try
            {                
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
            return product;
            // return Helper.MapToProductResponseDTO(product);
        }

        public async Task AddProduct(ProductCreateDTO productInput)
        {
            var productsJson = File.Exists(_productsFilePath) 
                ? await File.ReadAllTextAsync(_productsFilePath) 
                : "[]";
            var products = JsonSerializer.Deserialize<List<Product>>(productsJson) ?? new List<Product>();

             // Check if the name already exists (case-insensitive)
            if (products.Any(prod => prod.Name.Equals(productInput.Name, 
                StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException(
                    "A product with the same name already exists.");

           // ✅ ID generation stays in the service, mapping goes to Helper
            var newId   = products.Count > 0 ? products.Max(p => p.Id) + 1 : 1;
            var product = Helper.MapToProductEntity(productInput, newId);

             try
            {
                products.Add(product);
                await File.WriteAllTextAsync(_productsFilePath, JsonSerializer.Serialize(products, 
                    new JsonSerializerOptions { WriteIndented = true }));
            }
            catch (IOException)
            {
                // Handle file access issues
                throw;
            } 
        }

        public async Task UpdateProduct(int Id, ProductUpdateDTO productInput)
        {
            var productsJson = File.Exists(_productsFilePath) ? await File.ReadAllTextAsync(_productsFilePath) : "[]";
            var products = JsonSerializer.Deserialize<List<Product>>(productsJson) ?? new List<Product>();
            var existingProduct = products.FirstOrDefault(prod => prod.Id == Id)
            ?? throw new KeyNotFoundException("Product not found.");

             if (products.Any(prod => prod.Id != Id && 
                prod.Name.Equals(productInput.Name, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException(
                    "A product with the same name already exists.");

                existingProduct.Name        = productInput.Name;
                existingProduct.Price       = productInput.Price;
                existingProduct.Description = productInput.Description;
                existingProduct.Category    = productInput.Category;
            try
            {

                //saving
                 await File.WriteAllTextAsync(_productsFilePath, 
                 JsonSerializer.Serialize(products));
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

            try
            {                
            var ordersJson = File.Exists(_ordersFilePath) ? await File.ReadAllTextAsync(_ordersFilePath) : "[]";
            var productsJson = File.Exists(_productsFilePath) ? await File.ReadAllTextAsync(_productsFilePath) : "[]";


            var orders = JsonSerializer.Deserialize<List<Order>>(ordersJson) ?? new List<Order>();
            var products = JsonSerializer.Deserialize<List<Product>>(productsJson) ?? new List<Product>();

            var productLookup = products.ToDictionary(p => p.Id, p => p);


            return Helper.MapToOrderResponseDTOList(orders, productLookup);
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
            return order;
            // return Helper.MapToOrderResponseDTOList(order);
        }

        public async Task AddOrder(OrderCreateDTO orderInput)
        {
            // Load inventory (items.json)
            var itemsJson = File.Exists(_itemsFilePath) 
                ? await File.ReadAllTextAsync(_itemsFilePath) 
                : "[]";
            var inventory = JsonSerializer.Deserialize<List<Item>>(itemsJson) ?? new List<Item>();

            // Load existing orders (orders.json)
            var ordersJson = File.Exists(_ordersFilePath) 
                ? await File.ReadAllTextAsync(_ordersFilePath) 
                : "[]";
            var orders = JsonSerializer.Deserialize<List<Order>>(ordersJson) ?? new List<Order>();


            // Map DTO in Entity to validate stock and create order
           foreach (var ordered in orderInput.Items)
            {
                var stock = inventory.FirstOrDefault(i => i.ProductId == ordered.ProductId)
                    ?? throw new InvalidOperationException(
                        $"No inventory found for ProductId {ordered.ProductId}");

                if (stock.StockQuantity < ordered.Quantity)
                    throw new InvalidOperationException(
                        $"Not enough stock for ProductId {ordered.ProductId}. " +
                        $"Available: {stock.StockQuantity}, Requested: {ordered.Quantity}");

                // Deduct stock
                stock.StockQuantity -= ordered.Quantity;
            }
            // Create the new order
            var newOrder = new Order
            {
                Id           = orders.Count > 0 ? orders.Max(o => o.Id) + 1 : 1,
                CustomerName = orderInput.CustomerName,
                OrderDate    = DateTime.Now,
                OrderItems   = orderInput.Items.Select(item => new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity  = item.Quantity,
                    UnitPrice = item.UnitPrice
                }).ToList()
            };

             try
            {
                orders.Add(newOrder);
                // Save both files
                await File.WriteAllTextAsync(_ordersFilePath, JsonSerializer.Serialize(orders, 
                    new JsonSerializerOptions { WriteIndented = true }));
                await File.WriteAllTextAsync(_itemsFilePath, JsonSerializer.Serialize(inventory, 
                    new JsonSerializerOptions { WriteIndented = true }));

            }
            catch (IOException)
            {
                throw;
            }
        }
    
        public async Task UpdateOrder(int id, OrderUpdateDTO orderInput)
        {
           
            var ordersJson = File.Exists(_ordersFilePath) 
                            ? await File.ReadAllTextAsync(_ordersFilePath) 
                            : "[]";
            var orders = JsonSerializer.Deserialize<List<Order>>(ordersJson) ?? new List<Order>();
            var existingOrder = orders.FirstOrDefault(order => order.Id == id)
             ?? throw new KeyNotFoundException("Order not found.");

             // Load inventory
            var itemsJson = File.Exists(_itemsFilePath)
                ? await File.ReadAllTextAsync(_itemsFilePath)
                : "[]";
            var inventory = JsonSerializer.Deserialize<List<Item>>(itemsJson) ?? new List<Item>();

            // Restore old stock
            foreach (var oldItem in existingOrder.OrderItems)
            {
                var stock = inventory.FirstOrDefault(i => i.ProductId == oldItem.ProductId);
                if (stock != null)
                    stock.StockQuantity += oldItem.Quantity;
            }

            // Validate and deduct new stock
            foreach (var newItem in orderInput.Items)
            {
                var stock = inventory.FirstOrDefault(i => i.ProductId == newItem.ProductId)
                    ?? throw new InvalidOperationException(
                        $"No inventory found for ProductId {newItem.ProductId}");

                if (stock.StockQuantity < newItem.Quantity)
                    throw new InvalidOperationException(
                        $"Not enough stock for ProductId {newItem.ProductId}. " +
                        $"Available: {stock.StockQuantity}, Requested: {newItem.Quantity}");

                stock.StockQuantity -= newItem.Quantity;
            }

            // Update the order
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
                await File.WriteAllTextAsync(_ordersFilePath, JsonSerializer.Serialize(orders, 
                    new JsonSerializerOptions { WriteIndented = true }));
                await File.WriteAllTextAsync(_itemsFilePath, JsonSerializer.Serialize(inventory,
                    new JsonSerializerOptions { WriteIndented = true }));
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


//items
        public async Task<IEnumerable<ItemResponseDTO>> GetAllItems()
        {
            var itemsJson = File.Exists(_itemsFilePath) ? await File.ReadAllTextAsync(_itemsFilePath) : "[]";

            try
            {                
            var items = JsonSerializer.Deserialize<List<OrderItem>>(itemsJson) ?? new List<OrderItem>();
            return items.Select(item => new ItemResponseDTO
            {
                Id = item.Id,
                ProductId = item.ProductId,
                ProductName = item.Product?.Name ?? string.Empty,
                StockQuantity = item.Quantity
            }).ToList();
            }
            catch (JsonException)
            {
                // JSON is corrupted 
                return new List<ItemResponseDTO>();
            }
            catch (IOException)
            {
                // File access issues
                throw;
            }
        }

        public async Task<ItemResponseDTO> GetItemById(int id)
        {
            var items = await GetAllItems();
            var item = items.FirstOrDefault(item => item.Id == id) ?? throw new KeyNotFoundException($"Item with ID {id} not found.");
            return item;
        }

        public async Task AddItem(ItemCreateDTO itemInput)
        {
            var itemsJson = File.Exists(_itemsFilePath) ? await File.ReadAllTextAsync(_itemsFilePath) : "[]";
            var items = JsonSerializer.Deserialize<List<OrderItem>>(itemsJson) ?? new List<OrderItem>();

            var newItem = new OrderItem
            {
                Id = items.Count > 0 ? items.Max(item => item.Id) + 1 : 1,
                ProductId = itemInput.ProductId,
                Quantity = itemInput.Quantity,
            };

             try
            {
                items.Add(newItem);
                await File.WriteAllTextAsync(_itemsFilePath, JsonSerializer.Serialize(items, 
                    new JsonSerializerOptions { WriteIndented = true }));
            }
            catch (IOException)
            {
                throw;
            }
        }

        public async Task UpdateItem(int id, ItemUpdateDTO itemInput)
        {
            var itemsJson = File.Exists(_itemsFilePath) ? await File.ReadAllTextAsync(_itemsFilePath) : "[]";
            var items = JsonSerializer.Deserialize<List<OrderItem>>(itemsJson) ?? new List<OrderItem>();
            var existingItem = items.FirstOrDefault(item => item.Id == id)
             ?? throw new KeyNotFoundException("Item not found.");

            existingItem.Quantity = itemInput.Quantity;

            try
            {
                await File.WriteAllTextAsync(_itemsFilePath, JsonSerializer.Serialize(items, 
                new JsonSerializerOptions { WriteIndented = true }));
            }
            catch (IOException)
            {
                throw;
            }
        }
        
        public async Task DeleteItem(int id)
        {
            var items = (await GetAllItems()).ToList();
            var itemToRemove = items.FirstOrDefault(item => item.Id == id)
            ?? throw new KeyNotFoundException("Item not found.");

            try
            {
                items.Remove(itemToRemove);
                var serializedItems = JsonSerializer.Serialize(items, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(_itemsFilePath, serializedItems);
            }
            catch (IOException)
            {
                throw;
            } 
        }
    }
}