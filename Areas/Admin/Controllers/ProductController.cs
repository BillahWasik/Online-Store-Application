using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Store_Application.Areas.Admin.Models;
using Online_Store_Application.Repository;

namespace Online_Store_Application.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _db;
        private readonly IWebHostEnvironment _env;
        public ProductController(IProductRepository db , IWebHostEnvironment env)
        {
            this._db = db;
            this._env = env;
        }
        public async Task<IActionResult> Index()
        {
           var data = await _db.GetProductAsync();
            return View(data);
        }
        public async Task<IActionResult> GetDetails(int id)
        {
          var data = await _db.GetProductDetails(id);
            return View(data);  
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Product obj)
        {
            if(obj != null)
            {
                string path = "Image/Product/";
               var ImageUrl = UploadImage(path,obj.Image);
                obj.ImageUrl= ImageUrl;

                var data = await _db.AddProduct(obj);
                RedirectToAction("Index");

            }

            return View();
        }
        public async Task<IActionResult> Edit(int id)
        {
            var data = await _db.GetProductDetails(id);
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Product obj)
        {

            if (obj != null)
            {
                var data = await _db.EditProduct(obj);
                return RedirectToAction("Index"); 
            }

                return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var data = await _db.GetProductDetails(id);
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Product obj)
        {
            if (obj != null)
            {
                await _db.DeleteProduct(obj);
                RedirectToAction("Index");
            }
            return View();
        }

        private string UploadImage(string path , IFormFile obj)
        {
            
            path += Guid.NewGuid().ToString() + " " + obj.FileName;

            string FullPath = Path.Combine(_env.WebRootPath,path);

            obj.CopyToAsync(new FileStream(FullPath, FileMode.Create));

            return path;

        }
    }
}
