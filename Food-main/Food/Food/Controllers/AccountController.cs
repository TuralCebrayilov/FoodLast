using Food.Helper;
using Food.Models;
using Food.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Food.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(UserManager<AppUser> userManager,RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;

            _signInManager = signInManager;

        }
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignInAsync(SignInVM signInVM)
        {
            AppUser user = await _userManager.FindByNameAsync(signInVM.Username);
            if (User == null)
            {
                ModelState.AddModelError("", "İstifadəci adı və ya şifrə yalnışdır");
                return View();
            }
            if (user.IsDeactive)
            {
                ModelState.AddModelError("", "İstifadəçi adı tapılmadı");
                return View();
            }
            Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signInManager.PasswordSignInAsync(user, signInVM.Password, signInVM.IsRemember, true);
            if (signInResult.IsLockedOut)
            {


                ModelState.AddModelError("", "İstifadəçi adı Bloklanmışdır");
                return View();

            }
            if (!signInResult.Succeeded)
            {
                ModelState.AddModelError("", "İstifadəci adı və ya şifrə yalnışdır");
                return View();

            }
            var roles = await _userManager.GetRolesAsync(user);

            // Eğer kullanıcı admin rolüne sahipse admin paneline yönlendir
            //if (roles.Contains("Admin"))
            //{
            //}
                return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
            
            //else
            //{
            //    return RedirectToAction("Index", "Home");
            //}
        }
        public IActionResult SignUn()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUn(SignUpVM signUpVM)
        {
            AppUser newUser = new AppUser
            {
                UserName = signUpVM.Username,
                Name = signUpVM.Name,
                Email = signUpVM.Email,
                Surname=signUpVM.SurName





            };
           IdentityResult identityResult= await _userManager.CreateAsync(newUser, signUpVM.Password);
            if(!identityResult.Succeeded)
            {
                foreach (IdentityError error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);

                }
                return View();

            }
            await _userManager.AddToRoleAsync(newUser, Roles.Admin.ToString());
            await _signInManager.SignInAsync(newUser, signUpVM.IsRemember);
            return RedirectToAction("Index","Home");
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        //public async Task CreateRoles()
        //{
        //    if (!await _roleManager.RoleExistsAsync(Roles.Admin.ToString()))
        //    {
        //        await _roleManager.CreateAsync(new IdentityRole { Name = Roles.Admin.ToString() });
        //    }
        //    if (!await _roleManager.RoleExistsAsync(Roles.Member.ToString()))
        //    {
        //        await _roleManager.CreateAsync(new IdentityRole { Name = Roles.Member.ToString() });
        //    }
        //}

    }
}
