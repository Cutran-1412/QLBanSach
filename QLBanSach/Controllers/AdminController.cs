using Microsoft.AspNetCore.Mvc;

namespace QLBanSach.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
