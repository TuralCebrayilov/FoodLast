using Food.DAL;
using Food.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Food.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CommentController : Controller
    {
        private readonly AppDbContext _db;
        public CommentController(AppDbContext db)
        {
                
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            List<Comment> comments = await _db.Comments.ToListAsync();
            return View(comments);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Comment comment)
        {
            await _db.Comments.AddAsync(comment);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Comment _DbMenuProduct = await _db.Comments.FirstOrDefaultAsync(x => x.Id == id);
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
            Comment _DbMenuProduct = await _db.Comments.FirstOrDefaultAsync(x => x.Id == id);
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
            Comment _DbMenuProduct = await _db.Comments.FirstOrDefaultAsync(x => x.Id == id);
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
            Comment _dbcomment = await _db.Comments.FirstOrDefaultAsync(x => x.Id == id);
            if (_dbcomment == null)
            {
                return BadRequest();
            }

            return View(_dbcomment);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Comment comment, int CatId)
        {
            if (id == null)
            {
                return NotFound();
            }
            Comment _dbcomment = await _db.Comments.FirstOrDefaultAsync(x => x.Id == id);
            if (_dbcomment == null)
            {
                return BadRequest();
            }



            _dbcomment.NameCustoms = comment.NameCustoms;
            _dbcomment.CommentDescription = comment.CommentDescription;


            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Detail(int id)
        {
            Comment menuProduct = await _db.Comments.FindAsync(id);
            return View(menuProduct);
        }
    }
}
