using Data;
using Entities;
using Entities.Menu;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace shopWeb.Areas.Admin.Controllers
{
    [Area("admin")]

  //  [Authorize(Roles = "HRManager,Finance")]
    public class dashbordController : Controller
    {



      
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
            IWebHostEnvironment webHostEnvironment, IRepository<Menu> mymenu)
        {
            this.userRepository = userRepository;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
            this.WebHostEnvironment = webHostEnvironment;
            this._repositoryPost = _repositoryPost;
            this._mymenu = mymenu;


        }

        // GET: dashbordController
        public ActionResult Index()
        {
          var test=_repositoryPost.Table.ToList();

          
            return View();
        }

        // GET: dashbordController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: dashbordController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: dashbordController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: dashbordController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: dashbordController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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
