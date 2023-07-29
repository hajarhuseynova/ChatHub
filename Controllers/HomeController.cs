using ChatApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ChatApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
 
        private readonly UserManager<AppUser> _userManager;


        public HomeController( UserManager<AppUser> userManager)
        {
         
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            List<AppUser> users = await _userManager.Users.ToListAsync();
            return View(users);
        }

        //public async Task<IActionResult> CreateUser()
        //{
        //    await _userManager.CreateAsync(new AppUser
        //    {
        //        Email="hajarih@code.edu.az",
        //        UserName="HajarCode",
              
        //    },"Hajar123@");


        //    await _userManager.CreateAsync(new AppUser
        //    {
        //        Email = "huseynih@code.edu.az",
        //        UserName = "HuseynCode",

        //    }, "Huseyn123@");

        //    await _userManager.CreateAsync(new AppUser
        //    {
        //        Email = "punhanih@code.edu.az",
        //        UserName = "PunhanCode",

        //    }, "Punhan123@");

        //    await _userManager.CreateAsync(new AppUser
        //    {
        //        Email = "narmin@code.edu.az",
        //        UserName = "NarminCode",

        //    }, "Narmin123@");

        //    return Json("ok");
        //}

        
    }
}