using CasinoApp.Data;
using CasinoApp.Models;
using CasinoApp.Models.UtilityModels;
using CasinoApp.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace CasinoApp.Controllers
{
    public class CardGameController : Controller
    {
        private UserManager<AppUser>? _userManager;
        private SignInManager<AppUser>? _signInManager;
        IContext _db;
        private Deck _deck = new Deck();
        public CardGameController(IContext repo, UserManager<AppUser>? um, SignInManager<AppUser>? sm) { _db = repo; _userManager = um; _signInManager = sm; }

        public IActionResult Index()
        {
            List<Card> list = new List<Card>();
            _deck.Shuffle();
            list.Add(_deck.Deal());
            list.Add(_deck.Deal());
            list.Add(_deck.Deal());
            return View(list);
        }
        [HttpPost]
        public async Task<IActionResult> PlayCardGame(int input,int wager,int cardValue)
        { int oldValue = 0;
            List<Card> list = new List<Card>();
            _deck.Shuffle();
            list.Add(_deck.Deal());
            list.Add(_deck.Deal());
            list.Add(_deck.Deal());
            list.ForEach(card => { oldValue += card.Value; });
            if(input == 1 && cardValue > oldValue)
            {
               var u =  _userManager.GetUserAsync(User).Result;
                u.Coins += ((cardValue - oldValue) * wager);
                await _userManager.UpdateAsync(u);
                TempData["Message"] = $"You won {(cardValue - oldValue) * wager} coin!!";

                return View("Index",list);
            }
            else if (input == 0 && cardValue < oldValue)
            {
                var u = _userManager.GetUserAsync(User).Result;
                u.Coins += ((oldValue - cardValue) * wager);
                await _userManager.UpdateAsync(u);
                TempData["Message"] = $"You won {(oldValue - cardValue) * wager} coin!!";

                return View("Index", list);
            }
            else
            {
                var u = _userManager.GetUserAsync(User).Result;
                u.Coins -= wager;
                await _userManager.UpdateAsync(u);
                TempData["Message"] = $"You Lost {wager} coin!! But keep going!";

                return View("Index",list);
            }

        }
    }
}
