

using System.Text.Json;
using MyShop.DTO;
using MyShop.Entities;
using MyShop.Mapping;

namespace MyShop.Services
{
    public class StorageService : IStorageService
    {
        private readonly string _productsFilePath = "products.json";
        private readonly string _ordersFilePath = "orders.json";
        private readonly string _itemsFilePath = "items.json";

        public async Task<List<ProductResponseDTO>> GetAllProductsAsync()
        {
            if (!File.Exists(_productsFilePath))
            {
                return new List<ProductResponseDTO>();
            }

            var productsJson = await File.ReadAllTextAsync(_productsFilePath);
            var products = JsonSerializer.Deserialize<List<Product>>(productsJson) ?? new List<Product>();
            return Helper.MapToResponseDTOList(products);
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            if (!File.Exists(_ordersFilePath))
            {
                return new List<Order>();
            }

            var ordersJson = await File.ReadAllTextAsync(_ordersFilePath);
            var orders = JsonSerializer.Deserialize<List<Order>>(ordersJson) ?? new List<Order>();
            return orders;
        }

        public async Task<List<Item>> GetAllItemsAsync()
        {
            if (!File.Exists(_itemsFilePath))
            {
                return new List<Item>();
            }

            var itemsJson = await File.ReadAllTextAsync(_itemsFilePath);
            var items = JsonSerializer.Deserialize<List<Item>>(itemsJson) ?? new List<Item>();
            return items;
        }
    }
}