using Microsoft.EntityFrameworkCore;
using Online_Store_Application.Areas.Admin.Data;
using Online_Store_Application.Areas.Admin.Models;

namespace Online_Store_Application.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db)
        {
            this._db = db;
        }

        public async Task<List<Product>> GetProductAsync()
        {
            var data = _db.Products.Include(x => x.Category).ToListAsync();
            return await data;
        }
        public async Task<Product> GetProductDetails(int id)
        {
            var data = _db.Products.Where(x => x.Id == id).FirstOrDefaultAsync();
            return await data;
        }
        public async Task<Product> AddProduct(Product obj)
        {
            if (obj != null)
            {
                await _db.Products.AddAsync(obj);
                await _db.SaveChangesAsync();
                return obj;
            }
            return null;
        }
        public async Task<int> EditProduct(Product obj)
        {

                _db.Products.Update(obj);
                await _db.SaveChangesAsync();
                return obj.Id;
        }
        public async Task<int> DeleteProduct(Product obj)
        {

                _db.Products.Remove(obj);
                await _db.SaveChangesAsync();
                return obj.Id;
        }
    }
}
