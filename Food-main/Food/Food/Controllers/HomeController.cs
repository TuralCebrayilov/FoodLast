using Food.DAL;
using Food.Models;
using Food.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.Helpers;
using Food.Migrations;

namespace Food.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;

        public HomeController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            HomeVM homeVM = new HomeVM
            {
                HomeSlider = await _db.HomeSliders.Where(x => x.IsDeactive == false).ToListAsync(),
                Comment = await _db.Comments.ToListAsync(),
                MenuCategories = await _db.MenuCategories.Where(x => x.IsDeactive == false).ToListAsync(),
                Drinks = await _db.Drinks.ToListAsync(),
                MenuProducts = await _db.MenuProducts.Where(x => x.IsDeactive == false).ToListAsync(),
                Chef=await _db.Chefs.Where(x=>x.IsDeactive==false).ToListAsync(),
            };
            //ViewBag.MenuProducts = await _db.MenuProducts.Where(x => x.IsDeactive == false).ToListAsync();
            return View(homeVM);
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Models.MenuProduct menuProduct = await _db.MenuProducts.Where(x => x.IsDeactive == false).Where(x=> x.Id==id).FirstOrDefaultAsync();

            return Json(menuProduct);
        }

        public async Task<IActionResult> GetProductByCategory(int? id)
        {
            var products =  await _db.MenuProducts.Where(x=> x.MenuCategoryId == id).ToListAsync();

            if (products == null)
            {
                return NotFound();
            }
            else if(products.Count > 4)
            {
                products.Take(4);
            }
           
            return Json(products);
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult Order()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Order(SendEmailVM sendEmailVM)
        {
            SmtpClient client = new SmtpClient("smtp.yandex.com", 587);
            client.UseDefaultCredentials = false;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("tural.j@itbrains.edu.az", "qvuhdpkqlhtqfuvd");
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            MailMessage message = new MailMessage("tural.j@itbrains.edu.az", sendEmailVM.Email);
            message.Subject = "Product order detail";
            message.Body = $"Hörmətli, {sendEmailVM.Name +" " + sendEmailVM.Surname}! Sifarişiniz qeydə alınmışdır və məhsulunuz qısa zamanda təslim ediləcək.";
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;

            await client.SendMailAsync(message);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AddBenefit(double amount)
        {
            Benefit benefit = new();
            benefit.CreateTime = DateTime.Now;
            benefit.Amount = amount;
            benefit.Description = "New product";

            await _db.Benefits.AddAsync(benefit);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Models.Comment comment)
        {
            if (ModelState.IsValid)
            {
                await _db.Comments.AddAsync(comment);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(comment);
        }
        //public async Task<IActionResult> Chekout(string email)
        //{
        //    if (email == null)
        //    {
        //        return Content("Email is null");
        //    }

        //    Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        //    Match match = regex.Match(email);
        //    if (!match.Success)
        //    {
        //        return Content("This is not email");
        //    }
        //    else
        //    {
        //        Chekout chekout = new Chekout
        //        {
        //            Email = email,
        //        };
               
        //        await _db.Chekouts.AddAsync(chekout);
        //        await _db.SaveChangesAsync();

        //    }

        //    return Content("Ok");
        //}
     

    }
}