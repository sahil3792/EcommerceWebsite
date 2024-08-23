using System.Diagnostics;
using E_commerce_webApp.Data;
using E_commerce_webApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace E_commerce_webApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext db;
        private IWebHostEnvironment env;
        public ProductController(ApplicationDbContext db, IWebHostEnvironment env)
        {
            this.db = db;
            this.env = env;
        }

        public IActionResult Index(string choice)
        {
            if (choice == "All")
            {
                var data = db.Products.ToList();
                return View(data);
            }else if (choice == "Low to High")
            {
                var data = db.Products.OrderBy(x=>x.Price).ToList();
                return View(data);
            }else if(choice == "Top 5")
            {
                var data = db.Products.Take(3).ToList();
                return View(data);
            }
            else
            {
                var data =  db.Products.OrderByDescending(x=>x.Price).ToList();
                return View(data);  
            }
            

            
        }

        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(ProductViewModel pm)
        {
            var path = env.WebRootPath;
            var filePath = "Content/Images/" + pm.Picture.FileName;
            var fullPath = Path.Combine(path, filePath);
            UploadFile(pm.Picture, fullPath);
            var obj = new Product()
            {
                Pname = pm.Pname,
                Pcat = pm.Pcat,
                Price = pm.Price,
                Picture = filePath
            };
            db.Add(obj);
            db.SaveChanges();
            TempData["msg"] = "Product Added Sucessfully";

            return RedirectToAction("Index");


        }
        public void UploadFile(IFormFile file, string fullPath)
        {
            FileStream stream = new FileStream(fullPath, FileMode.Create);
            file.CopyTo(stream);
        }

        public IActionResult AddToCart(int id)
        {
            string sess = HttpContext.Session.GetString("MyUser");
            var prod = db.Products.Find(id);
            var obj = new Cart()
            {
                Pname = prod.Pname,
                Pcat = prod.Pcat,
                Picture = prod.Picture,
                Price = prod.Price,
                User = sess
            };
            db.Add(obj);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult ShowCart()
        {
            if(HttpContext.Session.GetString("MyUser").IsNullOrEmpty())
            {
                return RedirectToAction("SignIn", "Auth");
            }
            else
            {
                var sess = HttpContext.Session.GetString("MyUser");
                var prod = db.Carts.Where(x=>x.User == sess).ToList();
                return View(prod);

            }
            
        }
        public IActionResult DisplayCart()
        {
            if (HttpContext.Session.GetString("MyUser").IsNullOrEmpty())
            {
                return RedirectToAction("SignIn", "Auth");
            }
            else
            {
                var sess = HttpContext.Session.GetString("MyUser");
                var prod = db.Carts.Where(x => x.User == sess).ToList();
                return View(prod);

            }
        }
    }
}
