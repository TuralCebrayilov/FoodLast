using System.ComponentModel.DataAnnotations.Schema;

namespace Food.Models
{
    public class MenuCategory
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Image { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
        public List<MenuProduct>? Products { get; set; }
        public bool IsDeactive { get; set; }
    }
}
