using Data;
using Entities;
using Entities.Menu;
using Entities.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace shopWeb.Areas.Admin.Controllers
{
    [Area("admin")]
   // [Authorize(Roles ="admin")]
    //[Authorize(Roles = "HRManager,Finance")]
    public class dashbordController : Controller
    {



        private readonly IRepository<ProductCategory> _group;
        private readonly IRepository<Product> _product;

        private readonly IUserRepository userRepository;

        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly SignInManager<User> signInManager;

        private readonly IRepository<Post> _repositoryPost;
        private IWebHostEnvironment WebHostEnvironment { get; set; }

        private readonly IRepository<Menu> _mymenu;
        public dashbordController(IUserRepository userRepository,
            IRepository<Post> _repositoryPost, UserManager<User> userManager,
            RoleManager<Role> roleManager, SignInManager<User> signInManager,
            IWebHostEnvironment webHostEnvironment, IRepository<Menu> mymenu,
            IRepository<ProductCategory> _group,
            IRepository<Product> _product
            )
        {
            this.userRepository = userRepository;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
            this.WebHostEnvironment = webHostEnvironment;
            this._repositoryPost = _repositoryPost;
            this._mymenu = mymenu;
            this._group = _group;
            this._product = _product;



        }

        // GET: dashbordController
        
       

        // GET: dashbordController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }


        public ActionResult CreateGroup(int  id)
        {
            //ViewBag.createproduct = _group.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateGroup(ProductCategory model)
        {
            if (ModelState.IsValid)
            {
                _group.Add(model);
                await _group.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
            

        }



        // Get: dashbordController/Create
        public ActionResult CreateProduct()
        {

           

            return View();
        }
        // POST: dashbordController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                _product.Add(product);
                await _product.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        public ActionResult ProductList()
        {
            var resultTable = _product.Table.ToList();


            return View(resultTable);
        }


        // GET: dashbordController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: dashbordController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit(int id, Product model1)
        //{
           
        //    var test = _product.Table.FirstOrDefault(model1 => model1.Id == id);
        //    if(test == null)
        //    {
        //        return NotFound();
        //    }
        //    test.productName = model1.productName;
        //    test.productDescription = model1.productDescription;
        //    test.productSummery = model1.productSummery;
        //    test.IsSpecial = model1.IsSpecial;
        //    test.productCategory = model1.productCategory;
        //    test.Images = model1.Images;
        //    test.sizes = model1.sizes;
        //    test.colors = model1.colors;

        //        return View();
            
        //}

        // GET: dashbordController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: dashbordController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
