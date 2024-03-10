using System.ComponentModel.DataAnnotations.Schema;

namespace Food.Models
{
    public class MenuProduct
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public MenuCategory? MenuCategory { get; set; }
        public int MenuCategoryId { get; set; }
        public double ProductLPrice { get; set; }
        public double ProductMPrice { get; set; }
        public double ProductSPrice { get; set; }
        public bool IsDeactive { get; internal set; }

        public string? Image { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
