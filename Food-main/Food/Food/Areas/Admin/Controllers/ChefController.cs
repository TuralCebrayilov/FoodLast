using Food.DAL;
using Food.Helper;
using Food.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Food.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ChefController : Controller
    {
        private readonly AppDbContext _db;
        private readonly  IWebHostEnvironment _env;
        public ChefController(AppDbContext db,
            IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
            
        }
        public async Task<IActionResult> Index( )
        {
            List<Chef> chefs=await _db.Chefs.Include(x => x.Position).OrderByDescending(x => x.Id).ToListAsync();
            return View(chefs);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Positions = await _db.Positions.ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Chef chef, int CatId)
        {
            ViewBag.Positions = await _db.Positions.ToListAsync();

            #region Save Image


            if (chef.Photo == null)
            {
                ModelState.AddModelError("Photo", "Image can't be null!!");
                return View();
            }
            if (!chef.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Please select image type");
                return View();
            }
            if (chef.Photo == null)
            {
                ModelState.AddModelError("Photo", "max 1mb !!");
                return View();
            }

            string folder = Path.Combine(_env.WebRootPath, "assets", "images");
            chef.Images = await chef.Photo.SaveFileAsync(folder);
            #endregion
            #region Exist Item
            bool isExist = await _db.Chefs.AnyAsync(x => x.Name == chef.Name);
            if (isExist)
            {
                ModelState.AddModelError("Name", "This chef is already exist !");
                return View(chef);
            }
            #endregion
            chef.PositionId = CatId;
            await _db.Chefs.AddAsync(chef);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Chef _dbchef = await _db.Chefs.FirstOrDefaultAsync(x => x.Id == id);
            if (_dbchef == null)
            {
                return BadRequest();
            }
            return View(_dbchef);

        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Chef _dbchef = await _db.Chefs.FirstOrDefaultAsync(x => x.Id == id);
            if (_dbchef == null)
            {
                return BadRequest();
            }
            _dbchef.IsDeactive = true;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Chef _dbchef = await _db.Chefs.FirstOrDefaultAsync(x => x.Id == id);
            if (_dbchef == null)
            {
                return BadRequest();
            }
            if (_dbchef.IsDeactive)
            {
                _dbchef.IsDeactive = false;
            }
            else
            {
                _dbchef.IsDeactive = true;
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
            Chef _dbchef = await _db.Chefs.FirstOrDefaultAsync(x => x.Id == id);
            if (_dbchef == null)
            {
                return BadRequest();
            }
            ViewBag.MenuCategories = await _db.MenuCategories.ToListAsync();
            return View(_dbchef);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Chef chef, int CatId)
        {
            if (id == null)
            {
                return NotFound();
            }
            Chef _dbchef = await _db.Chefs.FirstOrDefaultAsync(x => x.Id == id);
            if (_dbchef == null)
            {
                return BadRequest();
            }
            ViewBag.Positions = await _db.Positions.ToListAsync();
            //#region Exist Item
            //bool isExist = await _Db.Teachers.AnyAsync(x => x.Name == teachers.Name && CatId != id);
            //if (isExist)
            //{
            //    ModelState.AddModelError("Name", "This teachers is already exist !");
            //    return View(teachers);
            //}
            //#endregion
            #region Save Image


            if (chef.Photo != null)
            {
                if (!chef.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Şəkil seçin!!");
                    return View();
                }
                if (chef.Photo == null)
                {
                    ModelState.AddModelError("Photo", "max 1mb !!");
                    return View();
                }
                string folder = Path.Combine(_env.WebRootPath, "assets", "images");
                _dbchef.Images = await chef.Photo.SaveFileAsync(folder);

            }

            #endregion
            _dbchef.Name = chef.Name;
            _dbchef.ChefFb = chef.ChefFb;
            

            _dbchef.PositionId = CatId;

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Detail(int id)
        {
            Chef menuProduct = await _db.Chefs.FindAsync(id);
            return View(menuProduct);
        }
    }
}
