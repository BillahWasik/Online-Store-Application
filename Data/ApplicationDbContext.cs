using Microsoft.EntityFrameworkCore;
using Online_Store_Application.Models;

namespace Online_Store_Application.Data
{
    public class ApplicationDbContext : DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
    }
}
