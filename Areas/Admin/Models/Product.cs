using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Store_Application.Areas.Admin.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
        [NotMapped]

        public IFormFile Image { get; set; } = null;
    }
}
