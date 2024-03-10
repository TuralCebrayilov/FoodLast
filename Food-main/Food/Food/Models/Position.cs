namespace Food.Models
{
    public class Position
    {
        public int Id { get; set; }
        public string PositionName { get; set; }
        public bool IsDeactive { get; set; }
        public List<Chef> Chefs { get; set;}
            }
}
