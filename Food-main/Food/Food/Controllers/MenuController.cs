using Food.DAL;
using Food.Models;
using Food.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Food.Controllers
{
    public class MenuController : Controller
    {
        private readonly AppDbContext _db;
        public MenuController(AppDbContext db)
        {
                _db = db;
        }
        public async Task<IActionResult> Index()
        {
            HomeVM homeVM = new HomeVM
            {
                HomeSlider = await _db.HomeSliders.Where(x => x.IsDeactive == false).ToListAsync(),
                Comment = await _db.Comments.ToListAsync(),
                MenuCategories = await _db.MenuCategories.Where(x => x.IsDeactive == false).ToListAsync(),
                Drinks = await _db.Drinks.ToListAsync(),
                MenuProducts = await _db.MenuProducts.Where(x => x.IsDeactive == false).ToListAsync(),
            };
            return View(homeVM);
        }

        public async Task<IActionResult> GetMenuByCategory(int? id)
        {
            MenuCategoryVM menuCategoryVM = new()
            {
                MenuCategories = await _db.MenuCategories.ToListAsync(),
                MenuProducts = await _db.MenuProducts.Where(x=>x.Id == id).ToListAsync()
            };

            return View(menuCategoryVM);
        }
    }
}
