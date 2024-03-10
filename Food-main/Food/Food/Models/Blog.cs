namespace Food.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string? BlogName { get; set; }
        public string? BlogDescription { get; set; }
        public string? BlogImage { get; set; }
        public DateTime BlogDateTime { get; set; }
        public string? Author { get; set; }
    }
}
