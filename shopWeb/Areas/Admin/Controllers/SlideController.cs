using Data;
using Data.Repositories;
using Entities;
using Entities.Menu;
using Entities.Slide;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shopWeb.Models;
using System.Collections;
using System.IO;
using static NuGet.Packaging.PackagingConstants;

namespace shopWeb.Areas.Admin.Controllers
{
    [Area("admin")]
    //[Authorize(Roles="admin")]
    public class SlideController : Controller
    {

        private const int MaxBufferSize = 0x10000;
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
            var firstname = "Robert'\'\'; DROP TABLE Menu; --";
            IQueryable<Entities.Menu.Menu> data = _mymenu.Table.Where(p=>p.NameMenu== firstname);
            var test = data.ToList();
            return View();
        }


        public ActionResult GetGridData([DataSourceRequest] DataSourceRequest request)
        {
            // Retrieve data from your data source
            List<op> lst = new List<op>();
            lst.Add(new op { Id = 1 });
            lst.Add(new op { Id = 2 });

            // Return the data as JSON
            return Json(lst);
        }

        public JsonResult Slide_Read([DataSourceRequest] DataSourceRequest request)
        {
            // List<Slide> res =  _repositorySlide.Table.AsNoTracking().ToList();
            List<op> lst = new List<op>();
            lst.Add(new op { Id = 1 });
            lst.Add(new op { Id = 2 });
            return Json(lst.ToDataSourceResult(request));
          
        }

      

        // GET: SlideController/Create
        public ActionResult CreateSlide(int ? id)
        {

            return View();
        }

        private static string GetUniqueFileName(IFormFile formFile)
        {
            var fileName = Path.GetFileNameWithoutExtension(formFile.FileName);
            var extension = Path.GetExtension(formFile.FileName);
            return $"{fileName}.{DateTime.Now.Ticks.ToString()}{extension}";
        }

        private static string GetFilePath(string fileName, string uploadsRootFolder)
        {
            return Path.Combine(uploadsRootFolder, fileName);
        }

        public async Task<bool> SaveFileAsync(IFormFile formFile, string filePath, bool isOverwrite = true)
        {
            string fileName = formFile.FileName;
            if (isOverwrite)
            {
                fileName = GetUniqueFileName(formFile);
            }

            string path = GetFilePath(fileName, filePath);
            //if (!isOverwrite)
            //{
            //    if (File.Exists(path))
            //        File.Delete(path);
            //}

            using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write,
                                        FileShare.None,
                                        MaxBufferSize,
                                        useAsync: true))
            {
                await formFile.CopyToAsync(fileStream);
            }
            return true;
        }



        // POST: SlideController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateSlide(Slide data)
        {

            //var extension = Path.GetExtension(file.FileName);
            //if (extension != null)
            //{
            //    var ext = extension.ToLower();
            //    string[] arrext;
            //    if (extensionFile == ExtensionFileEnum.Image)
            //        arrext = new string[] { ".jpg", ".png", ".jpeg", ".gif", ".svg" };
            //    else if (extensionFile == ExtensionFileEnum.Audio)
            //        arrext = new string[] { ".ogg", ".mp3" };
            //    else if (extensionFile == ExtensionFileEnum.Video)
            //        arrext = new string[] { ".ogg", ".mp4" };
            //    else if (extensionFile == ExtensionFileEnum.File)
            //        arrext = new string[] { ".zip", ".rar", ".pdf", ".txt", ".docx", ".doc" };
            //    else if (extensionFile == ExtensionFileEnum.Excel)
            //        arrext = new string[] { ".xls", ".xlsx" };
            //    else if (extensionFile == ExtensionFileEnum.Apk)
            //        arrext = new string[] { ".apk" };
            //    else
            //        arrext = new string[] { ".zip", ".rar", ".pdf", ".xls", ".xlsx", ".txt", ".gif", ".jpg", ".png", ".jpeg", ".txt", ".svg", ".ogg", ".mp3", ".mp4", ".docx", ".doc" };

            //    var pos = Array.IndexOf(arrext, ext);
            //    if (pos <= -1)
            //    {
            //        return new ResultViewModel { Succeeded = false, Message = "The file type is incorrect" };
            //    }
            //}
            //if (!(file.Length <= contentLength))
            //{
            //    return new ResultViewModel { Succeeded = false, Message = "The size of the submitted file is more than the allowed value" };
            //}

            await SaveFileAsync(data.UploadFiles, $"wwwroot\\files", true);
            data.imageUrl = "ddddd";
       
            _repositorySlide.Add(data);
         
        

            try
            {
                return RedirectToAction("SlideList");
            }
            catch
            {
                return View();
            }
        }

     

      

    }
}
