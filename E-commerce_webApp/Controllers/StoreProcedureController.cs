using E_commerce_webApp.Data;
using E_commerce_webApp.Migrations;
using E_commerce_webApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_commerce_webApp.Controllers
{
	public class StoreProcedureController : Controller
	{
		private readonly ApplicationDbContext db;
		private readonly IWebHostEnvironment env;
		public StoreProcedureController(ApplicationDbContext db,IWebHostEnvironment env)
		{
			this.db = db;
			this.env = env;	
		}
		public IActionResult Index()
		{
			var d =db.emps.FromSqlRaw("exec FetchUser").ToList();
			return View(d);
		}

		public IActionResult AddEmp()
		{
			return View();
		}

		[HttpPost]
		public IActionResult AddEmp(Emp e)
		{
			db.Database.ExecuteSqlRaw($"exec InsertEmp '{e.Name}','{e.Dept}',{e.Salary}");
			return RedirectToAction("Index");	
		}

		
	}
}
