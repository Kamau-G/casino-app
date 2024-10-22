using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using CasinoApp.Models;
using CasinoApp.Data;
using CasinoApp.Controllers;

namespace CommunitySite.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userMngr,
            SignInManager<AppUser> signInMngr)
        {
            _userManager = userMngr;
            _signInManager = signInMngr;
        }
        [HttpGet]
        public IActionResult LogIn(string? returnURL = null)
        {
            var model = new LoginViewModel { ReturnUrl = returnURL };
            return RedirectToAction(nameof(HomeController.LogIn),"Home",model);
        }

    }
}