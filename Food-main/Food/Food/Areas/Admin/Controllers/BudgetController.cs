using Food.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Food.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Food.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BudgetController : Controller
    {
        private readonly AppDbContext _Db;
      
        public BudgetController(AppDbContext Db)
        {
            _Db = Db;
           
        }
        public async Task<IActionResult> Index()
        {
            Budget budget = await _Db.Budgets.FirstOrDefaultAsync();
            return View(budget);
        }
    }
}
