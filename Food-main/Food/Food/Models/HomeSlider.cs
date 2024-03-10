using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Food.Models
{
    public class HomeSlider
    {

        public int Id { get; set; }
        public string? Title { get; set; }
        public string? SubTitle { get; set; }
        public string? Image { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
        public bool IsDeactive { get; internal set; }
    }
}
