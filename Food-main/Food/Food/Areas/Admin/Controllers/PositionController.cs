using Food.DAL;
using Food.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Food.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PositionController : Controller
    {
        private readonly AppDbContext _db;
        public PositionController(AppDbContext db)
        {
                _db = db;
            
        }
        public async Task<IActionResult> Index()
        {
            List<Position> positions = await _db.Positions.ToListAsync();
            return View(positions);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Position position)
        {
            await _db.Positions.AddAsync(position);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Position _DbMenuProduct = await _db.Positions.FirstOrDefaultAsync(x => x.Id == id);
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
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Position _dbposition = await _db.Positions.FirstOrDefaultAsync(x => x.Id == id);
            if (_dbposition == null)
            {
                return BadRequest();
            }

            return View(_dbposition);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Position position, int CatId)
        {
            if (id == null)
            {
                return NotFound();
            }
            Position _dbposition = await _db.Positions.FirstOrDefaultAsync(x => x.Id == id);
            if (_dbposition == null)
            {
                return BadRequest();
            }



            _dbposition.PositionName = position.PositionName;
        


            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Detail(int id)
        {
            Position menuProduct = await _db.Positions.FindAsync(id);
            return View(menuProduct);
        }
    }
}
