using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce_webApp.Controllers
{
    public class DashboardController : Controller
    {
        [Authorize]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]  
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

    }
}
