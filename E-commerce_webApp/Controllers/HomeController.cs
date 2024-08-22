using System.Diagnostics;
using E_commerce_webApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce_webApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            if(string.IsNullOrEmpty(HttpContext.Session.GetString("MyUser")))
            {
                return RedirectToAction("SignIn", "Auth");
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
    }


}
