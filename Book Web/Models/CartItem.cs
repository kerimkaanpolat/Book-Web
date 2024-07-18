using System.ComponentModel.DataAnnotations.Schema;

namespace Book_Web.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int Quantity { get; set; }
    }
}
