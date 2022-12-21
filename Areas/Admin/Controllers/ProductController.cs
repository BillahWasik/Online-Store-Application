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
            if(ModelState.IsValid)
            {
                //string Folder = "Image/Product";
                //Folder += Guid.NewGuid().ToString() + " " + obj.Image.FileName;

                //string ServerFolder = Path.Combine(_env.WebRootPath, Folder);

                //await obj.Image.CopyToAsync(new FileStream(ServerFolder, FileMode.Create));

                //obj.ImageUrl = Folder;

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
            if (ModelState.IsValid)
            {
                var data = await _db.EditProduct(obj);
                RedirectToAction("Index");
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
            if (ModelState.IsValid)
            {
                var data = await _db.DeleteProduct(obj);
                RedirectToAction("Index");
            }
            return View();
        }

        private void UploadImage(Product obj)
        {
            string Folder = "Image/Product";
            Folder += Guid.NewGuid().ToString() + " " + obj.Image.FileName;

            string ServerFolder = Path.Combine(_env.WebRootPath,Folder);

            obj.Image.CopyToAsync(new FileStream(ServerFolder, FileMode.Create));

            obj.ImageUrl = Folder;
        }
    }
}
