using Microsoft.AspNetCore.Mvc;
using Online_Store_Application.Data;
using Online_Store_Application.Models;

namespace Online_Store_Application.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            this._db = db;
        }
        public IActionResult Index()
        {
           var data = _db.Categories.ToList();
            return View(data);
        }
        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            _db.Categories.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var data = _db.Categories.Where(x=> x.Id== id).FirstOrDefault();
            return View(data);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            _db.Categories.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var data = _db.Categories.Where(x => x.Id == id).FirstOrDefault();
            return View(data);
        }
        [HttpPost]
        public IActionResult Delete(Category obj)
        {
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
