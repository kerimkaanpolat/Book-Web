using System.ComponentModel.DataAnnotations.Schema;

namespace Book_Web.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public Order Order { get; set; } = null!;  // Varsayılan değer eklendi
        public Book Book { get; set; } = null!;    // Varsayılan değer eklendi
    }
}
