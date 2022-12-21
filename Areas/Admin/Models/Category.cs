using System.ComponentModel.DataAnnotations;

namespace Online_Store_Application.Areas.Admin.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please Enter Total Order")]
        [Display(Name = "Total Order")]
        public int DisplayOrder { get; set; }
        [Display(Name = "Created")]
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
