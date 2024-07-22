using Microsoft.AspNetCore.Mvc;

namespace Book_Web.Pages.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
