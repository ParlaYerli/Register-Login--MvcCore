using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Register.DataContext;
using Register.Models;

namespace Register.Controllers
{
    public class HomeController : Controller
    {
        ContextUser context;
        UserManager<User> _userManager;
        SignInManager<User> _signInManager;
        public HomeController(UserManager<User> userManager, SignInManager<User> signInManager, ContextUser _context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            context = _context;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.Name,
                    Email = model.Email,
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                }
            }
            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginUser user)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(user.Name, user.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("List", "Home");
                }

            }
            return View(user);
        }


        [HttpGet]
        public IActionResult List()
        {
             List<User> list= context.Users.ToList();
             return View(list);
        }
        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
