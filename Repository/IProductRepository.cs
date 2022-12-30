using Online_Store_Application.Areas.Admin.Models;

namespace Online_Store_Application.Repository
{
    public interface IProductRepository
    {
        Task<Product> AddProduct(Product obj);
        Task<int> DeleteProduct(Product obj);
        Task<int> EditProduct(Product obj);
        Task<List<Product>> GetProductAsync();
        Task<Product> GetProductDetails(int id);
        Task<List<Product>> SearchByPrice(double? low, double? high);
    }
}