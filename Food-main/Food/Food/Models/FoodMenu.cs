using System.ComponentModel.DataAnnotations.Schema;

namespace Food.Models
{
    public class FoodMenu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Time { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
