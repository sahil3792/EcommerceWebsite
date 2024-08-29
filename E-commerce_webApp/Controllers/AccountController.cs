using System.Security.Claims;
using E_commerce_webApp.Data;
using E_commerce_webApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace E_commerce_webApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext db;
        public AccountController(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IActionResult SignUp()
        {
            return View();
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




        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var storedCookie = Request.Cookies.Keys;
            foreach (var key in storedCookie)
            {
                Response.Cookies.Delete(key);
            }
            return RedirectToAction("SignIn");
        }


        [HttpPost]
        public IActionResult SignIn(SignIn log)
        {
            var data = db.Users.Where(x => x.Email.Equals(log.Email)).SingleOrDefault();
            if (data != null)
            {
                bool us = data.Email.Equals(log.Email) && data.Password.Equals(log.Password);
                if (us)
                {
                    
                    HttpContext.Session.SetString("URole", data.Role);
                    var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, log.Email) },
                        CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    HttpContext.Session.SetString("MyUser", data.Email);
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    TempData["IncorrectPassword"] = "Incorrect Password";
                }
            }
            else
            {
                TempData["IncorrectEmail"] = "Incorrect Email";
            }
            return View();

        }


    }


    
}
