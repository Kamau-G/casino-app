using CasinoApp.Data;
using CasinoApp.Models;
using CasinoApp.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Diagnostics;

namespace CasinoApp.Controllers
{
    public class HomeController : Controller
    {
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        IContext _db;

        public HomeController(IContext repo, UserManager<AppUser> um, SignInManager<AppUser> sm)
        { _db = repo; _userManager = um; _signInManager = sm; }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [HttpPost]
        //update this to register user
        public async Task<IActionResult> Privacy(string Name,string Password)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    UserName = Name,
                    Email = "Example@Email.com",
                    Name = Name,
                };
                var result = await _userManager.CreateAsync(user, Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult LogIn(string? returnURL = null)
        {
            var model = new LoginViewModel { ReturnUrl = returnURL };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LoginViewModel model, string? returnURL = null)
        {
            if (ModelState.IsValid)
            {
                // check if the user can log in
                var result = await _signInManager.PasswordSignInAsync(
                    model.Username, model.Password, isPersistent: model.RememberMe,
                    lockoutOnFailure: false);
                // if the login worked
                if (result.Succeeded)
                {
                    // if the url is not null or empty and the url is local
                    if (!string.IsNullOrEmpty(returnURL) &&
                        Url.IsLocalUrl(returnURL))
                    {
                        // take the user to the url
                        return Redirect(returnURL);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            ModelState.AddModelError("", "Invalid username/password.");
            return View(model);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}