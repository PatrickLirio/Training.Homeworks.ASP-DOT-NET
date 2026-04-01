
using MyShop.DTO.Items;

namespace MyShop.Services.Interfaces
{
    public interface IItemService 
    {
        Task<IEnumerable<ItemResponseDTO>> GetAllItems();
        Task<ItemResponseDTO> GetItemById(int id);
        Task AddItem(ItemCreateDTO itemInput);
        Task UpdateItem(int id, ItemUpdateDTO itemInput);
        Task DeleteItem(int id);
    }
}