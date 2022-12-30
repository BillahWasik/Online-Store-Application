using Microsoft.AspNetCore.Mvc;
using Online_Store_Application.Models;
using Online_Store_Application.Repository;
using System.Diagnostics;

namespace Online_Store_Application.Areas.Customer.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository _db;

        public HomeController(IProductRepository db)
        {
            this._db = db; 
        }

        public async Task<IActionResult> Index()
        {
           var data = await _db.GetProductAsync();
            return View(data);
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