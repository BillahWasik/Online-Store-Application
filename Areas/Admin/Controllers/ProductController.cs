using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Online_Store_Application.Areas.Admin.Data;
using Online_Store_Application.Areas.Admin.Models;
using Online_Store_Application.Repository;

namespace Online_Store_Application.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _db;
        private readonly IWebHostEnvironment _env;
        private readonly ApplicationDbContext _context;
        public ProductController(IProductRepository db , IWebHostEnvironment env , ApplicationDbContext context)
        {
            this._db = db;
            this._env = env;
            this._context = context;
        }

        private IEnumerable<Category> CategoryFetch()
        {
            var data = _context.Categories.ToList();
            return data;
        }

        public async Task<IActionResult> Index()
        {
           var data = await _db.GetProductAsync();
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Index( double start , double end)
        {
            var data = await _db.SearchByPrice(start, end);
            return View(data);
        }
        public async Task<IActionResult> GetDetails(int id)
        {
          var data = await _db.GetProductDetails(id);
            return View(data);  
        }
        public IActionResult Create(bool IsSuccess =false)
        {
            TempData["success"] = "Category Added Successfully";
            ViewBag.Success = IsSuccess;
            ViewBag.Category = new SelectList(CategoryFetch(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product obj)
        {
            ViewBag.Category = new SelectList(CategoryFetch(), "Id", "Name");

            if (obj.Image != null)
            {
                obj.ImageUrl = "Image/No_Image_Found/NoImage.jpg";
            }
            if (obj.Image != null)
            {
                string path = "Image/Product/";
                path += Guid.NewGuid().ToString() + "_" + obj.Image.FileName;
                string FullPath = Path.Combine(_env.WebRootPath, path);

                if(obj.ImageUrl != null)
                {
                    var OldPath = Path.Combine(_env.WebRootPath, obj.ImageUrl);

                    if (System.IO.File.Exists(OldPath))
                    {
                        System.IO.File.Delete(OldPath);
                    }
                }

                await obj.Image.CopyToAsync(new FileStream(FullPath, FileMode.Create));
                obj.ImageUrl = path;

                var data = await _db.AddProduct(obj);
               return RedirectToAction("Index" , new { IsSuccess = true});

            }

            return View();
        }
        public async Task<IActionResult> Edit(int id , bool IsSuccess = false)
        {
            TempData["success"] = "Category Updated Successfully";
            ViewBag.Success = IsSuccess;
            ViewBag.Category = new SelectList(CategoryFetch(), "Id", "Name");
            var data = await _db.GetProductDetails(id);
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Product obj)
        {
            ViewBag.Category = new SelectList(CategoryFetch(), "Id", "Name");
            if (obj.Image != null)
            {
                string path = "Image/Product/";
                path += Guid.NewGuid().ToString() + "_" + obj.Image.FileName;
                string FullPath = Path.Combine(_env.WebRootPath, path);

                if (obj.ImageUrl != null)
                {
                    var OldPath = Path.Combine(_env.WebRootPath, obj.ImageUrl);

                    if (System.IO.File.Exists(OldPath))
                    {
                        System.IO.File.Delete(OldPath);
                    }
                }
                await obj.Image.CopyToAsync(new FileStream(FullPath, FileMode.Create));
                obj.ImageUrl = path; 
            }
            
                await _db.EditProduct(obj);
            return RedirectToAction("Index", new { IsSuccess = true });
        }

        public async Task<IActionResult> Delete(int id , bool IsSuccess = false)
        {
            TempData["success"] = "Category Deleted Successfully";
            ViewBag.Success = IsSuccess;
            ViewBag.Category = new SelectList(CategoryFetch(), "Id", "Name");
            var data = await _db.GetProductDetails(id);
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Product obj)
        {
            ViewBag.Category = new SelectList(CategoryFetch(), "Id", "Name");
            if (obj.ImageUrl != null)
            {
                var OldPath = Path.Combine(_env.WebRootPath, obj.ImageUrl);

                if (System.IO.File.Exists(OldPath))
                {
                    System.IO.File.Delete(OldPath);
                }


                await _db.DeleteProduct(obj);
                return RedirectToAction("Index", new { IsSuccess = true });
            }

            return View(obj);
        }
        
    }
}
