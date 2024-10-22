using CasinoApp.Data;
using CasinoApp.Models;
using CasinoApp.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CasinoApp.Controllers
{
    public class MessagePageController : Controller
    {
        private UserManager<AppUser>? _userManager;
        private SignInManager<AppUser>? _signInManager;
        IContext _db;
        public MessagePageController(IContext repo, UserManager<AppUser>? um, SignInManager<AppUser>? sm) { _db = repo; _userManager = um; _signInManager = sm; }
        public async Task<IActionResult> Index()
        {
            if (!Object.Equals(null, _userManager.GetUserAsync(User).Result))
            {
                var u = _userManager.GetUserAsync(User).Result;
                var messages = await _db.GetMessagesById(u.Id);
                return View(messages);
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> DetailedMessage(int index)
        {
            var m = await _db.GetMessagesById(_userManager.GetUserAsync(User).Result.Id);
            var message = m[index];
            return View(message);
        }

        [HttpPost]
        public async Task<IActionResult> Post(MessageVM msg)
        {
            var u = _userManager.GetUserAsync(User).Result;
            var a = _userManager.FindByNameAsync(msg.Rcpnt).Result;

            if (User.Identity.IsAuthenticated)
            {
                var m = new Message { Rcpnt = a, Sndr= u,Text=msg.Text };
                await _db.StoreMessageAsync(m);
                TempData["MsgStatus"] = "Message Stored";
                return RedirectToAction("Index");
            }
            TempData["MsgStatus"] = "Message Failed";

            return RedirectToAction("Index");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost]
        public async Task<IActionResult> Reply(int msgId, string Text)
        {
            var m = await _db.GetMessageById(msgId);
            if (ModelState.IsValid)
            {
                if (m != null)
                {
                    // Build the reply
                    var r = new Message{Rcpnt=m.Sndr, Sndr=m.Rcpnt,Text = Text,};
                    // add reply
                    m.Replies.Add(r);
                    // save
                    await _db.UpdateMessageAsync(m);
                    TempData["MsgStatus"] = "Reply Posted";
                    return RedirectToAction("Index");
                }
                else{
                    TempData["MsgStatus"] = "Something went wrong! (Error: MsgId)";
                    return RedirectToAction("Index");

                }
            }
            else{
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public async Task<IActionResult> DeleteMsg(int id)
        {
           await  _db.DeleteMessageByIdAsync(id);
            return RedirectToAction("Index");
        }
    }
}
