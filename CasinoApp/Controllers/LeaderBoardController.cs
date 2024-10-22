using CasinoApp.Data;
using CasinoApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CasinoApp.Controllers
{
    public class LeaderBoardController : Controller
    {
        // Check the Authorization headers or add an if statement for the user
        private UserManager<AppUser>? _userManager;
        private SignInManager<AppUser>? _signInManager;
        IContext _db;
        public LeaderBoardController(IContext repo, UserManager<AppUser>? um, SignInManager<AppUser>? sm) { _db = repo; _userManager = um; _signInManager = sm; }
        public async Task<IActionResult> Index()
        {
            var diceScores = await _db.GetDiceGameScores();
            diceScores = diceScores.OrderByDescending(s => s.Score).ToList();
            return View(diceScores);
        }
        [HttpPost][Authorize] public async Task<IActionResult> Index(string userName) {
            var list = await _db.GetDiceGameScoresByName(userName);
            list = list.OrderByDescending(s => s.Score).ToList();
            if (list != null)
            {
                ViewBag.User = _userManager.GetUserAsync(User).Result;
            return View("Index",list);
            }
            return RedirectToAction();
        }
    }
}
