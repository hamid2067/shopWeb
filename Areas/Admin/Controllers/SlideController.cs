using Data;
using Entities;
using Entities.Menu;
using Entities.Slide;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace shopWeb.Areas.Admin.Controllers
{
    [Area("admin")]
    public class SlideController : Controller
    {


        private readonly IUserRepository userRepository;

        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly SignInManager<User> signInManager;

        private readonly IRepository<Slide> _repositorySlide;
        private IWebHostEnvironment WebHostEnvironment { get; set; }

        private readonly IRepository<Entities.Menu.Menu> _mymenu;

        public SlideController(IUserRepository userRepository,
            IRepository<Slide> _repositorySlide, UserManager<User> userManager,
            RoleManager<Role> roleManager, SignInManager<User> signInManager,
            IWebHostEnvironment webHostEnvironment, IRepository<Entities.Menu.Menu> mymenu)
        {
            this.userRepository = userRepository;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
            this.WebHostEnvironment = webHostEnvironment;
            this._repositorySlide = _repositorySlide;
            this._mymenu = mymenu;
        }


        // GET: SlideController
        public ActionResult SlideList()
        {

            return View();
        }

        public JsonResult Slide_Read([DataSourceRequest] DataSourceRequest request)
        {
            List<Slide> res =  _repositorySlide.Table.AsNoTracking().ToList();
            return Json(res.ToDataSourceResult(request));
          
        }

        // GET: SlideController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SlideController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SlideController/Create
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

        // GET: SlideController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SlideController/Edit/5
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

        // GET: SlideController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SlideController/Delete/5
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
