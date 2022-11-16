using System.ComponentModel.DataAnnotations;

namespace FrontToBackFlowers.Areas.AdminPanel.Models
{
    public class CategoryCreateModel
    {
        public int Id { get; set; }
        [Required, MaxLength(20)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }
    }
}
