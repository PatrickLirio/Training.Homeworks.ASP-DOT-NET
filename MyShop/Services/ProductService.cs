
using MyShop.DTO;
using MyShop.Entities;
using MyShop.Mapping;
using MyShop.Repository.Interfaces;
using MyShop.Services.Interfaces;

namespace MyShop.Services
{
public class ProductService : IProductService
{
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductResponseDTO>> GetAllProducts()
        {
            var products = await _productRepository.GetAllAsync();
            return Helper.MapToProductResponseDTOList(products.ToList());
        }

        public async Task<ProductResponseDTO> GetProductById(int id)
        {
            var product = await _productRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"Product with ID {id} not found.");
            return Helper.MapToProductResponseDTO(product);
        }

        public async Task<ProductResponseDTO> GetProductByName(string name)
        {
            var product = await _productRepository.GetByNameAsync(name)
                ?? throw new KeyNotFoundException($"Product with name '{name}' not found.");
            return Helper.MapToProductResponseDTO(product);
        }

        public async Task<IEnumerable<ProductResponseDTO>> GetProductsByCategory(string category)
        {
            var products = await _productRepository.GetAllAsync();
            return Helper.MapToProductResponseDTOList(
                products.Where(p => p.Category.ToLower() == category.ToLower()).ToList());
        }

        public async Task AddProduct(ProductCreateDTO productInput)
        {
            if (await _productRepository.NameExistsAsync(productInput.Name))
                throw new InvalidOperationException("A product with the same name already exists.");

            var product = Helper.MapToProductEntity(productInput);
            await _productRepository.AddAsync(product);
        }

        public async Task UpdateProduct(int id, ProductUpdateDTO productInput)
        {
            var product = await _productRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Product not found.");

            if (await _productRepository.NameExistsAsync(productInput.Name, excludeId: id))
                throw new InvalidOperationException("A product with the same name already exists.");

            product.Name        = productInput.Name;
            product.Price       = productInput.Price;
            product.Description = productInput.Description;
            product.Category    = productInput.Category;

            await _productRepository.UpdateAsync(product);
        }

        public async Task DeleteProduct(int id)
        {
            var product = await _productRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Product not found.");
            await _productRepository.DeleteAsync(product);
        }

    }
}