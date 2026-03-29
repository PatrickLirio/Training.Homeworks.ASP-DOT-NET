using System.ComponentModel.DataAnnotations;

namespace MyShop.DTO.Items
{
    public class ItemUpdateDTO
    {
        public string? ProductName { get; set; }
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
    }
}