using Food.DAL;
using Food.Migrations;
using Food.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Food.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DrinksController : Controller
    {
        private readonly AppDbContext _Db;

        public DrinksController(AppDbContext Db)
        {
            _Db = Db;

        }

        public async Task<IActionResult> Index()
        {
            List<Drink> drinks= await _Db.Drinks.ToListAsync();
            return View(drinks);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Drink drink)
        {
            await _Db.Drinks.AddAsync(drink);
            await _Db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Drink _DbMenuProduct = await _Db.Drinks.FirstOrDefaultAsync(x => x.Id == id);
            if (_DbMenuProduct == null)
            {
                return BadRequest();
            }
            if (_DbMenuProduct.IsDeactive)
            {
                _DbMenuProduct.IsDeactive = false;
            }
            else
            {
                _DbMenuProduct.IsDeactive = true;
            }
            await _Db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Drink _Dbdrink = await _Db.Drinks.FirstOrDefaultAsync(x => x.Id == id);
            if (_Dbdrink == null)
            {
                return BadRequest();
            }
           
            return View(_Dbdrink);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Drink drink, int CatId)
        {
            if (id == null)
            {
                return NotFound();
            }
            Drink _Dbdrink = await _Db.Drinks.FirstOrDefaultAsync(x => x.Id == id);
            if (_Dbdrink == null)
            {
                return BadRequest();
            }
         

            
            _Dbdrink.Name = drink.Name;
            _Dbdrink.Price = drink.Price;
           

            await _Db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Detail(int id)
        {
            Drink menuProduct = await _Db.Drinks.FindAsync(id);
            return View(menuProduct);
        }

    }
}
