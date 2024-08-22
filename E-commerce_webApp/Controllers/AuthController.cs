using E_commerce_webApp.Data;
using E_commerce_webApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce_webApp.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext db;
        public AuthController(ApplicationDbContext db)
        {
            this.db = db;
        }

        
        public IActionResult SignUp()
        {
            return View();
        }

        [AcceptVerbs("Post", "Get")]
        public IActionResult CheckExistingEmailID(string email)
        {
            var data = db.Users.Where(x => x.Email == email).SingleOrDefault();
            if (data != null)
            {
                return Json($"Email{email} Already in use");

            }
            else
            {
                return Json(true);
            }
        }

        

        [HttpPost]
        public IActionResult SignUp(User u)
        {
            u.Role = "User";
            db.Users.Add(u);
            db.SaveChanges();
            return RedirectToAction("SignIn");
        }


        public IActionResult SignIn()
        {

            return View();
        }

        [HttpPost]
        public IActionResult SignIn(User u)
        {
            var data = db.Users.Where(x => x.Email == u.Email && x.Password == u.Password);
            if (data != null)
            {
                return RedirectToAction("SignUp");
            }
            else
            {
                return View();
            }

        }
        
    }
}
