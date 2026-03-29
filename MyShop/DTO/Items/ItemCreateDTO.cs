
using System.ComponentModel.DataAnnotations;

namespace MyShop.DTO.Items
{
    public class ItemCreateDTO
    {
        [Required]
        public int ProductId { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Quantity cannot be negative")]
        public int Quantity { get; set; }
    }
}