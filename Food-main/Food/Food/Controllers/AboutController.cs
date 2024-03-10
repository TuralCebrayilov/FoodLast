using Food.DAL;
using Food.Models;
using Food.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Food.Controllers
{
    public class AboutController : Controller
    {
        private readonly AppDbContext _db;
        public AboutController(AppDbContext db)
        {
          _db = db;
        }
        public async Task<IActionResult> Index()
        {
            HomeVM homeVM = new HomeVM
            {
               
                Comment = await _db.Comments.ToListAsync(),
              
              About=await _db.Abouts.ToListAsync(),
                Chef = await _db.Chefs.Where(x => x.IsDeactive == false).ToListAsync(),
            };

            return View(homeVM);
        }
    }
}
