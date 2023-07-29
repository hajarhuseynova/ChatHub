using ChatApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ChatApp.Controllers
{
    public class AccountController : Controller
    {
   
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signinManager;

        public AccountController( UserManager<AppUser> userManager = null, SignInManager<AppUser> signinManager = null)
        {
            _userManager = userManager;
            _signinManager = signinManager;
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

       [HttpPost]
       public async Task<IActionResult> Login(AppUser appUser)
            {

             AppUser? user= await _userManager.FindByNameAsync(appUser.UserName);

             var result= await _signinManager.PasswordSignInAsync(user, appUser.PasswordHash,true,true);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "username or password incorrrect");
                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signinManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }


    }
}