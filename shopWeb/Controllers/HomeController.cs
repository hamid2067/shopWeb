using Data;
using Data.Repositories;
using Entities;
using Entities.Product;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shopWeb.Models;
using System.Diagnostics;

namespace shopWeb.Controllers
{
    public class HomeController : Controller
    {

        private readonly IUserRepository userRepository;
        private readonly IRepository<Product> _product;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly SignInManager<User> signInManager;

        public HomeController(IRepository<Product> product,IUserRepository userRepository,
            UserManager<User> userManager,
            RoleManager<Role> roleManager, SignInManager<User> signInManager)
        {
            this.userRepository = userRepository;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
            this._product = product;


        }

        private readonly ILogger<HomeController> _logger;

        

        public IActionResult registerAdmin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> registerAdmin(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.Email, Email = model.Email,
                    FullName = model.fullName, NatinalCode = model.NatinalCode };
                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var roleExit = await roleManager.RoleExistsAsync("User");

                    if (!roleExit)
                    {
                        var role = new Role { Name = "User", Description = "tttt" };
                        await roleManager.CreateAsync(role);
                    }

                    await userManager.AddToRoleAsync(user, "User");

                    await signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }

            return View(model);
        }


        public IActionResult LoginAdmin()
        {
            

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAdmin(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result=await signInManager.PasswordSignInAsync(model.Email, model.Password, model.Rememberme ,false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt");
            }
            return View(model);
        }

        public async Task<IActionResult>  LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ProductDetails(int ? id)
        {

            var prd = _product.Table.Include(p => p.pips).Include(p => p.Images).Where(p => p.Id == id).FirstOrDefault();

            return View(prd);
        }


        public IActionResult Index()
        {

          var importantprd= _product.Table.Include(p=>p.pips).Include(p=>p.Images).Where(p=>p.IsSpecial==true).ToList();
         
            return View(importantprd);
        }

        public IActionResult Index2()
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