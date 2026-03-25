using System.Text.Json;
using MyShop.DTO;
using MyShop.Entities;
using MyShop.Mapping;
using MyShop.Services.Interfaces;

namespace MyShop.Services
{
    public class ProductService : IProductService
    {

        public readonly string _filePath = "products.json";

        public async Task<IEnumerable<ProductResponseDTO>> GetAllProductsAsync()
        {
                if (!File.Exists(_filePath))
            {
                return Enumerable.Empty<ProductResponseDTO>();
            }

            try
            {
                var json = await File.ReadAllTextAsync(_filePath);
                var products = JsonSerializer.Deserialize<List<Product>>(json) ?? new List<Product>();
                return products.Select(Helper.MapToResponseDTO);
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


        public async Task<ProductResponseDTO> GetProductByIdAsync(int id)
        {
            var products = await GetAllProductsAsync();
            // return products.FirstOrDefault(p => p.Id == id);
            var product  = products.FirstOrDefault(p => p.Id == id)
                ?? throw new KeyNotFoundException($"Product with ID {id} not found.");
            return Helper.MapToResponseDTO(product);
        }

        public async Task AddProductAsync(ProductCreateDTO productInput)
        {
            var productsJson = await File.ReadAllTextAsync(_filePath);
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
                await File.WriteAllTextAsync(_filePath, serializedProducts);
            }
            catch (IOException)
            {
                // Handle file access issues
                throw;
            } 
        }

        public async Task UpdateProductAsync(ProductUpdateDTO productInput)
        {
            var products = (await GetAllProductsAsync()).ToList();
            var existingProduct = products.FirstOrDefault(p => p.Id == productInput.Id)
                ?? throw new KeyNotFoundException("Product not found.");

            
            if (products.Any(p => p.Id != productInput.Id && 
                p.Name.Equals(productInput.Name, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException(
                    "A product with the same name already exists.");

            try
            {
                
                existingProduct.Name        = productInput.Name;
                existingProduct.Price       = productInput.Price;
                existingProduct.Description = productInput.Description;
                existingProduct.Category    = productInput.Category;

                await File.WriteAllTextAsync(_filePath, JsonSerializer.Serialize(products));
            }
            catch (IOException) { throw; }
        }

        public async Task DeleteProductAsync(int id)
        {
            var products = (await GetAllProductsAsync()).ToList();
            var productToRemove = products.FirstOrDefault(p => p.Id == id);
            if (productToRemove == null)
                throw new KeyNotFoundException("Product not found.");

            try
            {
                products.Remove(productToRemove);
                var serializedProducts = JsonSerializer.Serialize(products);
                await File.WriteAllTextAsync(_filePath, serializedProducts);
            }
            catch (IOException)
            {
                // Handle file access issues
                throw;
            } 
        }
    }
}