using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Book_Web.Data;
using Book_Web.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Web.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CartController> _logger;

        public CartController(ApplicationDbContext context,ILogger<CartController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Sepeti Görüntüleme
        public async Task<IActionResult> Index()
        {
            var cartItems = await _context.CartItems
                                          .Include(c => c.Book)
                                          .ToListAsync();
            return View(cartItems);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart(int bookId)
        {
            if (bookId <= 0)
            {
                return BadRequest();
            }

            var book = await _context.Books.FindAsync(bookId);
            if (book == null)
            {
                return NotFound();
            }

            var cartItem = await _context.CartItems.FirstOrDefaultAsync(c => c.BookId == bookId);
            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    BookId = bookId,
                    Quantity = 1
                };
                _context.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity++;
            }

            book.Stock--;
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var cartItem = await _context.CartItems.Include(c => c.Book).FirstOrDefaultAsync(c => c.Id == id);
            if (cartItem == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(cartItem.BookId);
            book.Stock += cartItem.Quantity;

            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        // Sipariş Verme
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(string CustomerName, string Address, string City, string PostalCode)
        {
            if (string.IsNullOrEmpty(CustomerName) || string.IsNullOrEmpty(Address) || string.IsNullOrEmpty(City) || string.IsNullOrEmpty(PostalCode))
            {
                return View();
            }

            var cartItems = await _context.CartItems.Include(c => c.Book).ToListAsync();
            if (cartItems.Count == 0)
            {
                return RedirectToAction(nameof(Index));
            }

            var order = new Order
            {
                OrderDate = DateTime.Now,
                CustomerName = CustomerName,
                Address = Address,
                City = City,
                PostalCode = PostalCode,
                Items = new List<OrderItem>() // OrderItem listesi oluşturun
            };

            foreach (var item in cartItems)
            {
                order.Items.Add(new OrderItem
                {
                    BookId = item.BookId,
                    Quantity = item.Quantity
                });
            }

            _context.Orders.Add(order);
            _context.CartItems.RemoveRange(cartItems);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Hata durumunda gerekli işlemler
                throw;
            }

            return RedirectToAction("Index", "Orders");
        }
    }
}
