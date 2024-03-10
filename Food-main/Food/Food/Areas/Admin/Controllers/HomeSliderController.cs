using Food.DAL;
using Food.Helper;
using Food.Migrations;
using Food.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using HomeSlider = Food.Models.HomeSlider;

namespace Food.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeSliderController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
     
        public HomeSliderController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
     
        public async Task<IActionResult> Index()
        {
            List<Models.HomeSlider> homeSliders = await _db.HomeSliders.ToListAsync();
            return View(homeSliders);
        }
      
        public IActionResult Create()
        {
            return View();
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Models.HomeSlider homeSliders)
        {
            #region Save Image


            if (homeSliders.Photo == null)
            {
                ModelState.AddModelError("Photo", "Image can't be null!!");
                return View();
            }
            if (!homeSliders.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Please select image type");
                return View();
            }
            if (homeSliders.Photo == null)
            {
                ModelState.AddModelError("Photo", "max 1mb !!");
                return View();
            }

            string folder = Path.Combine(_env.WebRootPath, "assets", "images");
            homeSliders.Image = await homeSliders.Photo.SaveFileAsync(folder);
            #endregion

            await _db.HomeSliders.AddAsync(homeSliders);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Models.HomeSlider _DbHomeSlider = await _db.HomeSliders.FirstOrDefaultAsync(x => x.Id == id);
            if (_DbHomeSlider == null)
            {
                return BadRequest();
            }
            return View(_DbHomeSlider);

        }
      
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Models.HomeSlider _DbHomeSlider = await _db.HomeSliders.FirstOrDefaultAsync(x => x.Id == id);
            if (_DbHomeSlider == null)
            {
                return BadRequest();
            }
            _DbHomeSlider.IsDeactive = true;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
      
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Models.HomeSlider _DbHomeSlider = await _db.HomeSliders.FirstOrDefaultAsync(x => x.Id == id);
            if (_DbHomeSlider == null)
            {
                return BadRequest();
            }
            if (_DbHomeSlider.IsDeactive)
            {
                _DbHomeSlider.IsDeactive = false;
            }
            else
            {
                _DbHomeSlider.IsDeactive = true;
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
           Models.HomeSlider _DbHomeSlider = await _db.HomeSliders.FirstOrDefaultAsync(x => x.Id == id);
            if (_DbHomeSlider == null)
            {
                return BadRequest();
            }
            ViewBag.MenuCategories = await _db.MenuCategories.ToListAsync();
            return View(_DbHomeSlider);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, HomeSlider homeSlider, int CatId)
        {
            if (id == null)
            {
                return NotFound();
            }
          Models.HomeSlider  _DbHomeSlider = await _db.HomeSliders.FirstOrDefaultAsync(x => x.Id == id);
            if (_DbHomeSlider == null)
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


            if (homeSlider.Photo != null)
            {
                if (!homeSlider.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Şəkil seçin!!");
                    return View();
                }
                if (homeSlider.Photo == null)
                {
                    ModelState.AddModelError("Photo", "max 1mb !!");
                    return View();
                }
                string folder = Path.Combine(_env.WebRootPath, "assets", "images");
                _DbHomeSlider.Image = await homeSlider.Photo.SaveFileAsync(folder);

            }

            #endregion
            _DbHomeSlider.Image = homeSlider.Image;
          

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
