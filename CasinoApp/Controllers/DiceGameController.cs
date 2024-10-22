using Microsoft.AspNetCore.Mvc;
using CasinoApp.Models;
using CasinoApp.Data;
using System.Dynamic;
using Microsoft.VisualBasic;
using CasinoApp.Models.UtilityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace CasinoApp.Controllers
{
    public class DiceGameController : Controller
    {
        private UserManager<AppUser>? _userManager;
        private SignInManager<AppUser>? _signInManager;
        IContext _db;
        Dice _dice = new Dice();
        AppUser? _user;
        List<DiceGameScore> _scores = new List<DiceGameScore>();
        public DiceGameController(IContext repo, UserManager<AppUser>? um, SignInManager<AppUser>? sm) { _db = repo; _userManager = um; _signInManager = sm; }
        [Authorize]
        public IActionResult Index()
        {
            dynamic model = new ExpandoObject();
            List<int>list = new List<int> { _dice.Roll(), _dice.Roll(), _dice.Roll(),_dice.Roll(), _dice.Roll(), _dice.Roll(), };
            model.Rolls = list;
            model.isSaved = null;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Post(string input,int count)
        {
                int i = 0;
                dynamic model = new ExpandoObject();
                model.isSaved = null;
                List<int> list = new List<int> { _dice.Roll(), _dice.Roll(), _dice.Roll(), _dice.Roll(), _dice.Roll(), _dice.Roll(), };

                list.ForEach(x => {if (x == 1){i++;}});
            // Add code below
                model.Rolls = list;
                if (input == "Play")
                {
                var ints = new List<int> { 0, 0, 0, 0, 0, 0 };
                TempData["Count"] = (i >= 2)?0:count;
                ViewData["Message"] = (i>=2) ?"You rolled two die of value : 1":"Keep rolling!";
                model.Rolls = (i>=2)? ints:list;
                return View("Index",model);
                }
                else
                {
                    var player = _userManager.GetUserAsync(User).Result;
                    var score = new DiceGameScore { Score = count, Player = player };
                    //save the game and go home
                    await _db.StoreDiceGameAsync(score);
                    model.isSaved = $"Game Saved as {player.UserName.ToString()}";
                    return View("Index", model);
                }

           
        }
    }
}
