using System.ComponentModel.DataAnnotations.Schema;

namespace Food.Models
{
    public class About
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public string? Image2 { get; set; }
        public string? Image3 { get; set; }
        public string? Image4 { get; set; }
        public string? Image5 { get; set; }
        public string? Image6 { get; set; }
        public string? Image7 { get; set; }
        public string? Image8 { get; set; }
        
        [NotMapped]
        public IFormFile Photo { get; set; }
        //public bool? IsDeactive { get; set; }

    }
}
