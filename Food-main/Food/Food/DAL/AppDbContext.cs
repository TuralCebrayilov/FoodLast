using Food.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Food.DAL
{
    public class AppDbContext: IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<MenuCategory> MenuCategories { get; set; }
        public DbSet<Chef> Chefs { get; set; }
     
        public DbSet<About> Abouts { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<HomeSlider> HomeSliders { get; set; }
        public DbSet<MenuProduct> MenuProducts { get; set; }
        public DbSet<Drink> Drinks { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Cost> Costs { get; set; }
        public DbSet<Benefit> Benefits { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<Chekout> Chekouts { get; set; }

       
    }
}
