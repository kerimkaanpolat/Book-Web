namespace Book_Web.Models
{
    public class User
    {public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int IsActive { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;

    }
}
