using DevFest22Asyut.Models;
using DevFest22Asyut.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DevFest22Asyut.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        
        
        public AdminController(
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            SignInManager<User> signInManager
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager
                .FindByEmailAsync(model.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "No User Exist With These Credentials");
                return View(model);
            }
            
            if (!await _userManager.CheckPasswordAsync(user, model.Password))
            {
                ModelState.AddModelError("", "Email Or Password is Invalid");
                return View(model);
            }


            await _signInManager
                .PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, false);


            if (string.IsNullOrWhiteSpace(model.ReturnUrl))
                return RedirectToAction("Index", "Home");
            else
                return Redirect(model.ReturnUrl);
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
