using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using CasinoApp.Models;
using CasinoApp.Data;
using CasinoApp.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Xml.Linq;

namespace CommunitySite.Controllers{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller{

        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        private RoleManager<IdentityRole> _rollManager;
        private IContext _db;
        public AdminController(UserManager<AppUser> userMngr,SignInManager<AppUser> signInMngr, RoleManager<IdentityRole> rollMngr){
            _userManager = userMngr;
            _signInManager = signInMngr;
            _rollManager = rollMngr;
        }
        [HttpGet]
        
        public async Task<IActionResult> Index(){
			List<AppUser> users = new List<AppUser>();
			List<AppUser> admins = new List<AppUser>();
            foreach (AppUser user in _userManager.Users.ToList<AppUser>())
			{
				user.RoleNames = await _userManager.GetRolesAsync(user);
                if (user.RoleNames.Contains("Admin")){
                admins.Add(user);
                }
                else
                {
                    users.Add(user);
                }
            }
			AdminVM usersToView = new AdminVM
			{
                Admins = admins,
				Users = users,
				Roles = _rollManager.Roles
			};
			return View(usersToView);
        }
        [HttpPost]
        public async Task<IActionResult> AddAdmin(string? name="") {
            if (!string.IsNullOrEmpty(name))
            {
                //get user via id
                var u = await _userManager.FindByNameAsync(name);
                // add user to Admin role store result
                if (!Object.ReferenceEquals(u,null))
                {
                    var result = await _userManager.AddToRoleAsync(u, "Admin");
                    TempData["Message"] = (result.Succeeded) ? $"{u.Name} added to Admin role" : $"Something went wrong!{result.Errors.FirstOrDefault()?.Description}";
                    return RedirectToAction("Index");
                }
                // Generate message with results

                TempData["Message"] = (u == null) ? "User not found" : String.Empty;
                // return to admin view with Message
                return RedirectToAction("Index");
            }
            TempData["Message"] = "No Data Submited!";
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> RemoveAdmin(string? name = "")
        {
            if (!string.IsNullOrEmpty(name))
            {
                //get user via id
                var u = await _userManager.FindByNameAsync(name);
                if (!Object.ReferenceEquals(u, null))
                {
                    // remove user from role store result
                    var result = await _userManager.RemoveFromRoleAsync(u, "Admin");
                    // Generate message with results
                    TempData["Message"] = (result.Succeeded) ? $"{u.Name} removed from Admin role" : $"Something went wrong!{result.Errors.FirstOrDefault()?.Description}";
                    // return to admin view with Message
                    return RedirectToAction("Index");
                }
                TempData["Message"] = (u == null) ? "User not found" : String.Empty;
                return RedirectToAction("Index");
            }
            TempData["Message"]  = "No Data Submited!";
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DeleteRole(string? role="")
        {
            if (!string.IsNullOrEmpty(role))
            {
                var r = await _rollManager.FindByNameAsync(role);
                if (!Object.ReferenceEquals(r, null))
                {
                    var result = await _rollManager.DeleteAsync(r);
                    TempData["Message"] = (result.Succeeded) ? $"{role} role removed." : $"Something went wrong!{result.Errors.FirstOrDefault()?.Description}";
                    return RedirectToAction("Index", ViewBag);
                }
                TempData["Message"] = (r == null) ? "Role not found" : String.Empty;
                return RedirectToAction("Index");
            }
            TempData["Message"] = "No Data Submited!";
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DeleteUser(string? name = "")
        {
            if (!string.IsNullOrEmpty(name)){
                    var u = await _userManager.FindByNameAsync(name);
                if (!Object.ReferenceEquals(u, null))
                {
                    //var r = await _db.DeleteGamesByUser(u.Id);
                    //var r2 = await _db.DeleteAllMsgByUserId(u.Id);
                    var result = await _userManager.DeleteAsync(u);
                    TempData["Message"] = (result.Succeeded) ? $"{u.Name} deleted." : $"Something went wrong!{result.Errors.FirstOrDefault()?.Description}";
                    return RedirectToAction("Index");
                }
                TempData["Message"] = (u == null) ? "User not found" : String.Empty;
                return RedirectToAction("Index");
            }

            TempData["Message"] = "No Data Submited!";
            return RedirectToAction("Index");
        }
    }
}