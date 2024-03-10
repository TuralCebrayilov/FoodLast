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
    public class CategoryController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public CategoryController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;

            _env = env;

        }
        public async Task<IActionResult> Index()
        {
           List<MenuCategory> menuCategories =await _db.MenuCategories.ToListAsync();
      
            return View(menuCategories);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MenuCategory menuCategory)
        {
            await _db.MenuCategories.AddAsync(menuCategory);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            MenuCategory _dbMenuCategory = await _db.MenuCategories.FirstOrDefaultAsync(x => x.Id == id);
            if (_dbMenuCategory == null)
            {
                return BadRequest();
            }
            return View(_dbMenuCategory);

        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            MenuCategory _dbMenuCategory = await _db.MenuCategories.FirstOrDefaultAsync(x => x.Id == id);
            if (_dbMenuCategory == null)
            {
                return BadRequest();
            }
            _dbMenuCategory.IsDeactive = true;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            MenuCategory _dbMenuCategory = await _db.MenuCategories.FirstOrDefaultAsync(x => x.Id == id);
            if (_dbMenuCategory == null)
            {
                return BadRequest();
            }
            if (_dbMenuCategory.IsDeactive)
            {
                _dbMenuCategory.IsDeactive = false;
            }
            else
            {
                _dbMenuCategory.IsDeactive = true;
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
            MenuCategory _dbMenuCategory = await _db.MenuCategories.FirstOrDefaultAsync(x => x.Id == id);
            if (_dbMenuCategory == null)
            {
                return BadRequest();
            }
            ViewBag.MenuCategories = await _db.MenuCategories.ToListAsync();
            return View(_dbMenuCategory);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, MenuCategory menuCategory, int CatId)
        {
            if (id == null)
            {
                return NotFound();
            }
            MenuCategory _dbMenuCategory = await _db.MenuCategories.FirstOrDefaultAsync(x => x.Id == id);
            if (_dbMenuCategory == null)
            {
                return BadRequest();
            }
            
            //#region Exist Item
            //bool isExist = await _Db.Teachers.AnyAsync(x => x.Name == teachers.Name && CatId != id);
            //if (isExist)
            //{
            //    ModelState.AddModelError("Name", "This teachers is already exist !");
            //    return View(teachers);
            //}
            //#endregion
            #region Save Image


            if (menuCategory.Photo != null)
            {
                if (!menuCategory.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Şəkil seçin!!");
                    return View();
                }
                if (menuCategory.Photo == null)
                {
                    ModelState.AddModelError("Photo", "max 1mb !!");
                    return View();
                }
                string folder = Path.Combine(_env.WebRootPath, "assets", "images");
                _dbMenuCategory.Image = await menuCategory.Photo.SaveFileAsync(folder);

            }

            #endregion
            _dbMenuCategory.Name = menuCategory.Name;
           

           

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Detail(int id)
        {
            MenuCategory menuCategory = await _db.MenuCategories.FindAsync(id);
            return View(menuCategory);
        }
    }
}
