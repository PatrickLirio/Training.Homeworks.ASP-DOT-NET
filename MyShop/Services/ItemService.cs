
using MyShop.DTO.Items;
using MyShop.Entities;
using MyShop.Mapping;
using MyShop.Repository.Interfaces;
using MyShop.Services.Interfaces;

namespace MyShop.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IProductRepository _productRepository;
        public ItemService(IItemRepository itemRepository, IProductRepository productRepository)
        {
            _itemRepository = itemRepository;
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ItemResponseDTO>> GetAllItems()
        {
            var items = await _itemRepository.GetAllAsync();
            var products = await _productRepository.GetAllAsync();
            var productLookup = products.ToDictionary(p => p.Id, p => p);
            return items.Select(item => Helper.MapToItemResponseDTO(item, productLookup)).ToList();
        }

        public async Task<ItemResponseDTO> GetItemById(int id)
        {
            var item = await _itemRepository.GetByIdAsync(id)
                ?? throw new Exception($"Item with id {id} not found.");
            var products = await _productRepository.GetAllAsync();
            var productLookup = products.ToDictionary(p => p.Id, p => p);
            return Helper.MapToItemResponseDTO(item, productLookup);
        }

        public async Task<ItemResponseDTO> GetItemByName(string name)
        {
            var item = await _itemRepository.GetByNameAsync(name)
                ?? throw new Exception($"Item with name {name} not found.");
            var products = await _productRepository.GetAllAsync();
            var productLookup = products.ToDictionary(p => p.Id, p => p);
            return Helper.MapToItemResponseDTO(item, productLookup);
        }

        public async Task AddItem(ItemCreateDTO itemInput)
        {
            var newId = (await _itemRepository.GetAllAsync()).Max(i => i.Id) + 1;
            var item = new Item
            {
                Id = newId,
                ProductId = itemInput.ProductId,
                StockQuantity = itemInput.Quantity
            };
            await _itemRepository.AddAsync(item);
        }

        public async Task UpdateItem(int id, ItemUpdateDTO itemInput)
        {
            var item = await _itemRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Item not found.");

            item.StockQuantity = itemInput.Quantity;

            await _itemRepository.UpdateAsync(item);
        }

        public async Task DeleteItem(int id)
        {
            var item = await _itemRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Item not found.");
            await _itemRepository.DeleteAsync(item);
        }
        
    }
}