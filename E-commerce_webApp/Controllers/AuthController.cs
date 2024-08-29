using System.Net.NetworkInformation;
using System.Text;
using E_commerce_webApp.Data;
using E_commerce_webApp.Models;
using Microsoft.AspNetCore.Authentication;
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

        public static string EncryptPassword(string password)
        {
            if(string.IsNullOrEmpty(password))
            {
                return null;
            }
            else
            {
                byte[] pass = ASCIIEncoding.ASCII.GetBytes(password);
                string encrpass = Convert.ToBase64String(pass);
                return encrpass;
            }
        }

        [HttpPost]
        public IActionResult SignUp(User u)
        {
            u.Password = EncryptPassword(u.Password);
            u.Role = "User";
            db.Users.Add(u);
            db.SaveChanges();
            return RedirectToAction("SignIn");
        }


        public IActionResult SignIn()
        {
            
            return View();
        }


        public static string DecrptPassword(string password)
        {
            if(string.IsNullOrEmpty(password))
            {
                return null ;

            }
            else
            {
                byte[] pass = Convert.FromBase64String   (password);
                string decrpass = ASCIIEncoding.ASCII.GetString(pass);
                return decrpass;
            }
        }

        [HttpPost]
        public IActionResult SignIn(SignIn log)
        {
            var data = db.Users.Where(x=> x.Email.Equals(log.Email)).SingleOrDefault();
            if (data != null)
            {
                bool us = data.Email.Equals(log.Email) && DecrptPassword(data.Password).Equals(log.Password);
                if (us)
                {
                    HttpContext.Session.SetString("MyUser", data.Email);
                    HttpContext.Session.SetString("URole", data.Role);
                    return RedirectToAction("Index", "Home");
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

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            HttpContext.Session.Clear();
            return RedirectToAction("SignIn", "Auth");   
        }
        
    }
}
