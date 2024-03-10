using Food.DAL;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Food.Models;
using Microsoft.AspNetCore.Authorization;

namespace Food.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BenefitController : Controller
    {
        private readonly AppDbContext _Db;

        public BenefitController(AppDbContext Db )
        {
            _Db = Db;

        }
        public async Task<IActionResult> Index()
        {
            List<Benefit> benefits = await _Db.Benefits.OrderByDescending(x => x.Id).ToListAsync();




            return View(benefits);
        }
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Benefit benefit)
        {
            if (benefit.Amount <= 0)
            {
                ModelState.AddModelError("Amount", "Məbləğ düzgün daxil edilməyib.");
                return View();
            }
            benefit.By = User.Identity.Name;
            Budget budget = await _Db.Budgets.FirstOrDefaultAsync();
            budget.LastModifiedDescription = benefit.Description;
            budget.LastModifiedDate = DateTime.UtcNow.AddHours(4);
            budget.LastModifiedAmount = benefit.Amount;
            budget.LastModifiedBy = benefit.By;
            budget.TotalBudget += benefit.Amount;

            await _Db.Benefits.AddAsync(benefit);
            await _Db.SaveChangesAsync();
            return RedirectToAction("Index");

        }
    }
}
