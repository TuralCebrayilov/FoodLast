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

    public class AboutController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public AboutController(AppDbContext db, IWebHostEnvironment env)
        {

            _db = db;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<About> abouts=await _db.Abouts.ToListAsync();
            
            return View(abouts);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(About menu, int CatId)
        {
           

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
         
           
            await _db.Abouts.AddAsync(menu);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
       
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            About _DbAbout = await _db.Abouts.FirstOrDefaultAsync(x => x.Id == id);
            if (_DbAbout == null)
            {
                return BadRequest();
            }
            ViewBag.MenuCategories = await _db.MenuCategories.ToListAsync();
            return View(_DbAbout);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, About About, int CatId)
        {
            if (id == null)
            {
                return NotFound();
            }
            About _DbAbout = await _db.Abouts.FirstOrDefaultAsync(x => x.Id == id);
            if (_DbAbout == null)
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


            if (About.Photo != null)
            {
                if (!About.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Şəkil seçin!!");
                    return View();
                }
                if (About.Photo == null)
                {
                    ModelState.AddModelError("Photo", "max 1mb !!");
                    return View();
                }
                string folder = Path.Combine(_env.WebRootPath, "assets", "images");
                _DbAbout.Image = await About.Photo.SaveFileAsync(folder);

            }

            #endregion
            _DbAbout.Title = About.Title;
            _DbAbout.Description = About.Description;
            _DbAbout.Image = About.Image;
            _DbAbout.Image2 = About.Image2;
            _DbAbout.Image3 = About.Image3;
            _DbAbout.Image4 = About.Image4;
            _DbAbout.Image5 = About.Image5;
            _DbAbout.Image8 = About.Image6;
            _DbAbout.Image7 = About.Image7;
            _DbAbout.Image8 = About.Image8;


           
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
