using System.ComponentModel.DataAnnotations.Schema;

namespace Food.Models
{
    public class Chef
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Images { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
        public Position? Position { get; set; }
        public int PositionId { get; set; }
        public bool IsDeactive { get; set; }
        public string? ChefFb { get; set; }
        public string? ChefTwitter { get; set; }
        public string?   ChefLinkedin { get; set; }


    }
}
