using Food.DAL;
using Food.Helper;
using Food.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Food.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MenuProductController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        public MenuProductController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            List<MenuProduct> MenuProducts = await _db.MenuProducts.ToListAsync();
            return View(MenuProducts);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.MenuCategories = await _db.MenuCategories.ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MenuProduct menu, int CatId)
        {
            ViewBag.Positions = await _db.Positions.ToListAsync();

            #region Save Image


            if (menu.Photo == null)
            {
                ModelState.AddModelError("Photo", "Image can't be null!!");
                return View();
            }
            if (!menu.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Please select image type");
                return View();
            }
            if (menu.Photo == null)
            {
                ModelState.AddModelError("Photo", "max 1mb !!");
                return View();
            }

            string folder = Path.Combine(_env.WebRootPath, "assets", "images");
            menu.Image = await menu.Photo.SaveFileAsync(folder);
            #endregion
            #region Exist Item
            bool isExist = await _db.MenuProducts.AnyAsync(x => x.ProductName == menu.ProductName);
            if (isExist)
            {
                ModelState.AddModelError("Name", "This chef is already exist !");
                return View(menu);
            }
            #endregion
            menu.MenuCategoryId = CatId;
            await _db.MenuProducts.AddAsync(menu);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            MenuProduct _DbMenuProduct = await _db.MenuProducts.FirstOrDefaultAsync(x => x.Id == id);
            if (_DbMenuProduct == null)
            {
                return BadRequest();
            }
            return View(_DbMenuProduct);

        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            MenuProduct _DbMenuProduct = await _db.MenuProducts.FirstOrDefaultAsync(x => x.Id == id);
            if (_DbMenuProduct == null)
            {
                return BadRequest();
            }
            _DbMenuProduct.IsDeactive = true;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            MenuProduct _DbMenuProduct = await _db.MenuProducts.FirstOrDefaultAsync(x => x.Id == id);
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
            MenuProduct _DbMenuProduct = await _db.MenuProducts.FirstOrDefaultAsync(x => x.Id == id);
            if (_DbMenuProduct == null)
            {
                return BadRequest();
            }
            ViewBag.MenuCategories = await _db.MenuCategories.ToListAsync();
            return View(_DbMenuProduct);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, MenuProduct menuProduct, int CatId)
        {
            if (id == null)
            {
                return NotFound();
            }
            MenuProduct _DbMenuProduct = await _db.MenuProducts.FirstOrDefaultAsync(x => x.Id == id);
            if (_DbMenuProduct == null)
            {
                return BadRequest();
            }
            ViewBag.MenuCategories = await _db.MenuCategories.ToListAsync();
            //#region Exist Item
            //bool isExist = await _Db.Teachers.AnyAsync(x => x.Name == teachers.Name && CatId != id);
            //if (isExist)
            //{
            //    ModelState.AddModelError("Name", "This teachers is already exist !");
            //    return View(teachers);
            //}
            //#endregion
            #region Save Image


            if (menuProduct.Photo != null)
            {
                if (!menuProduct.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Şəkil seçin!!");
                    return View();
                }
                if (menuProduct.Photo == null)
                {
                    ModelState.AddModelError("Photo", "max 1mb !!");
                    return View();
                }
                string folder = Path.Combine(_env.WebRootPath, "assets", "images");
                _DbMenuProduct.Image = await menuProduct.Photo.SaveFileAsync(folder);

            }

            #endregion
            _DbMenuProduct.ProductName = menuProduct.ProductName;
            _DbMenuProduct.ProductDescription = menuProduct.ProductDescription;
            _DbMenuProduct.ProductSPrice = menuProduct.ProductSPrice;
            _DbMenuProduct.ProductLPrice = menuProduct.ProductLPrice;
            _DbMenuProduct.ProductMPrice = menuProduct.ProductMPrice;
           

            _DbMenuProduct.MenuCategoryId = CatId;

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Detail (int id)
        {
            MenuProduct menuProduct = await _db.MenuProducts.FindAsync(id);
            return View(menuProduct);
        }
    }
}
