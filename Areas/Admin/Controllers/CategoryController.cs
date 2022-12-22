using Microsoft.AspNetCore.Mvc;
using Online_Store_Application.Areas.Admin.Data;
using Online_Store_Application.Areas.Admin.Models;
using Online_Store_Application.Repository;

namespace Online_Store_Application.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _db;
        public CategoryController(ICategoryRepository db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _db.GetCategoryAsync();
            return View(data);
        }
        public async Task<IActionResult> Create(bool IsSuccess = false)
        {
            ViewBag.Success = IsSuccess;
            TempData["success"] = "Category Added Successfully";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                await _db.AddCategory(obj);
                TempData["success"] = "Category Added Successfully";
                return RedirectToAction("Index", new {IsSuccess = true});
            }
            return View();

        }
        public async Task<IActionResult> Edit(int id , bool IsSuccess = false)
        {
            ViewBag.Success = IsSuccess;
            TempData["success"] = "Category Added Successfully";
            var data = await _db.GetCategoryDetails(id);
            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                await _db.EditCategory(obj);
                TempData["success"] = "Category Updated Successfully";
                return RedirectToAction("Index",  new { IsSuccess = true });
            }
            return View();

        }
        public async Task<IActionResult> Delete(int id , bool IsSuccess = false)
        {
            ViewBag.Success = IsSuccess;
            TempData["success"] = "Category Deleted Successfully";
            var data = await _db.GetCategoryDetails(id);
            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Category obj)
        {
            await _db.DeleteCategory(obj);
            TempData["success"] = "Category Deleted Successfully";
            return RedirectToAction("Index", new { IsSuccess = true });
        }
    }
}
