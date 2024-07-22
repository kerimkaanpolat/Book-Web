using Book_Web.Data;
using Book_Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Book_Web.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<OrdersController> _logger;

        public async Task<IActionResult> Index()
        {
            var orders = await _context.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Book)
                .ToListAsync();
            return View(orders);
        }

        public OrdersController(ApplicationDbContext context, ILogger<OrdersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Checkout()
        { 
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(Order order)
        {
            _logger.LogInformation("Checkout POST method called");

            if (ModelState.IsValid)
            {
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Order saved successfully");
                return RedirectToAction("Confirmation");
            }

            _logger.LogWarning("ModelState is invalid");
            return View(order);
        }

        public IActionResult Confirmation()
        {
            return View();
        }
    }
}