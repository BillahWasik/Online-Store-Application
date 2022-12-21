using Microsoft.EntityFrameworkCore;
using Online_Store_Application.Areas.Admin.Models;

namespace Online_Store_Application.Areas.Admin.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
    }
}
