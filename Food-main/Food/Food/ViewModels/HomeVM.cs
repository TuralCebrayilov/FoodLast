using Food.Migrations;

namespace Food.ViewModels
{
    public class HomeVM
    {
        public List<Food.Models.Chef> Chef { get; set; }
        public List<Food.Models.Comment> Comment { get; set; }
        public List<Food.Models.Drink> Drinks { get; set; }
        public List<Food.Models.MenuCategory> MenuCategories { get; set; }
        public List<Food.Models.MenuProduct> MenuProducts { get; set; }
        public List<Food.Models.Position> Positions { get; set; }
        public List<Food.Models.HomeSlider> HomeSlider { get; set; }
        public List<Food.Models.Blog> Blog { get; set; }
        public List<Food.Models.About> About { get; set; }
    }
}
