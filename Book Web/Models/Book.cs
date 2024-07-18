namespace Book_Web.Models
{
    public class Book
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Type { get; set; }
        public required string Author { get; set; }
        public decimal Price { get; set; }
        public required string Description { get; set; }
        public int Stock { get; set; }
        public required string Seller { get; set; }

    }
}
