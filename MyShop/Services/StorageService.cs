

using System.Text.Json;
using MyShop.DTO;
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
            return Helper.MapToResponseDTOList(products);
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
            return Helper.MapToResponseDTO(product);
        }

        public async Task AddProduct(ProductCreateDTO productInput)
        {
            var productsJson = await File.ReadAllTextAsync(_productsFilePath);
            var products = JsonSerializer.Deserialize<List<Product>>(productsJson) ?? new List<Product>();

             // Check if the name already exists (case-insensitive)
            if (products.Any(p => p.Name.Equals(productInput.Name, 
                StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException(
                    "A product with the same name already exists.");

            // Map DTO in Entity
            var product = new Product
            {
                Id          = products.Count > 0 ? products.Max(p => p.Id) + 1 : 1,
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

        public async Task UpdateProduct(int id, ProductUpdateDTO productInput)
        {
            var products = (await GetAllProducts()).ToList();
            var existingProduct = products.FirstOrDefault(prod => prod.Id == id)
             ?? throw new KeyNotFoundException("Product not found.");

             if (products.Any(prod => prod.Id != id && 
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
        
        public async Task DeleteProduct(int id)
        {
            var products = (await GetAllProducts()).ToList();
            var productToRemove = products.FirstOrDefault(prod => prod.Id == id)
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
        
    }
}