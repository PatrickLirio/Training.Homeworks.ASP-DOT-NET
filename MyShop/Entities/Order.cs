

using System.ComponentModel.DataAnnotations;

namespace MyShop.Entities
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public string CustomerName { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public decimal TotalAmount => OrderItems.Sum(i => i.UnitPrice * i.Quantity);
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        
    }
}