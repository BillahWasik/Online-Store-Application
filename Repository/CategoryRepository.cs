using Microsoft.EntityFrameworkCore;
using Online_Store_Application.Areas.Admin.Data;
using Online_Store_Application.Areas.Admin.Models;

namespace Online_Store_Application.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db)
        {
            this._db = db;
        }

        public async Task<List<Category>> GetCategoryAsync()
        {
            var data = _db.Categories.ToListAsync();
            return await data;
        }
        public async Task<Category> GetCategoryDetails(int id)
        {
            var data = _db.Categories.Where(x => x.Id == id).FirstOrDefaultAsync();
            return await data;
        }
        public async Task<Category> AddCategory(Category obj)
        {
            if (obj != null)
            {
                await _db.Categories.AddAsync(obj);
                await _db.SaveChangesAsync();
                return obj;
            }
            return null;
        }
        public async Task<int> EditCategory(Category obj)
        {

            if (obj != null)
            {
                _db.Categories.Update(obj);
                await _db.SaveChangesAsync();
            }
            return obj.Id;
        }
        public async Task<int> DeleteCategory(Category obj)
        {

            if (obj != null)
            {
                _db.Categories.Remove(obj);
                await _db.SaveChangesAsync();
            }
            return obj.Id;
        }
    }
}
