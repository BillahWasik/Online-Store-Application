using Online_Store_Application.Areas.Admin.Models;

namespace Online_Store_Application.Repository
{
    public interface ICategoryRepository
    {
        Task<Category> AddCategory(Category obj);
        Task<int> DeleteCategory(Category obj);
        Task<int> EditCategory(Category obj);
        Task<List<Category>> GetCategoryAsync();
        Task<Category> GetCategoryDetails(int id);
    }
}