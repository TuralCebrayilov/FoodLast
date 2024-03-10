namespace Food.Models
{
    public class Comment
    {

        public int Id { get; set; }
        public string? NameCustoms { get; set; }
        public string? CommentDescription { get; set; }
        public bool IsDeactive { get; set; } = true;
    }
}
 