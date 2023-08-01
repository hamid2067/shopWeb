using Data;
using Data.Repositories;
using Entities;
using Entities.Menu;
using Entities.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace shopWeb.Areas.Admin.Controllers
{
    [Area("admin")]
   // [Authorize(Roles ="admin")]
    //[Authorize(Roles = "HRManager,Finance")]
    public class dashbordController : Controller
    {
        private const int MaxBufferSize = 0x10000;
        private readonly IRepository<PIP> _pip;
        private readonly IRepository<imageProduct> _pic;
        private readonly IRepository<ProductSize> _size;
        private readonly IRepository<ProductColor> _color;
        private readonly IRepository<ProductCategory> _group;
        private readonly IRepository<Product> _product;

        private readonly IUserRepository userRepository;

        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly SignInManager<User> signInManager;

        private readonly IRepository<Post> _repositoryPost;
        private IWebHostEnvironment WebHostEnvironment { get; set; }

        private readonly IRepository<Menu> _mymenu;
        public dashbordController(IRepository<PIP> pip, IRepository<imageProduct> pic, IRepository<ProductSize> size, IRepository<ProductColor> color, IUserRepository userRepository,
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
            this._color = color;
            this._size = size;
            this._pic=pic;
            this._pip=pip;



        }

        public ActionResult ProductList()
        {
            var resultTable = _product.Table.ToList();


            return View(resultTable);
        }

        public ActionResult CreateProduct(int? id)
        {
            Product num = new();
            if (id != null)
            {
                var result = _product.Table.Where(p => p.Id == id).FirstOrDefault();
                if (result != null)
                {
                    num = result;
                }
            }
            ViewBag.group = _group.Table.ToList();
           
            return View(num);
        }


        public ActionResult colorproduct(int? id)
        {
         
          var result = _color.Table.Where(p => p.ProductId == id).ToList();
            ViewBag.ProductId = id;

            return View(result);
        }

        public ActionResult priceproduct(int? id)
        {

            var result = _pip.Table.Where(p => p.ProductId == id).ToList();
            ViewBag.ProductId = id;

            var sizelst = _size.Table.Where(p => p.ProductId == id).ToList();
            var colorlst = _color.Table.Where(p => p.ProductId == id).ToList();

            ViewBag.sizelst = sizelst;
            ViewBag.colorlst = colorlst;

            return View(result);
        }


        public ActionResult picproduct(int? id)
        {

            var result = _pic.Table.Where(p => p.productId == id).ToList();
            ViewBag.ProductId = id;

            return View(result);
        }

        public ActionResult addpicProduct(int? id)
        {
            ViewBag.ProductId = id;
            imageProduct img = new();
            return View(img);
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

        public async Task<string> SaveFileAsync(IFormFile formFile, string filePath, bool isOverwrite = true)
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
            return fileName;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> addpicProduct(imageProduct data)
        {

           
            if(data.IsFirst)
            {
              var res=  _pic.Table.Where(p => p.productId == data.productId).ToList();
                foreach (var item in res)
                {
                    item.IsFirst = false;
                }
            }
            data.urlProduct = await SaveFileAsync(data.UploadFiles, $"wwwroot\\files", true);
            _pic.Add(data);

            return RedirectToAction("picproduct",new {id= data.productId });
        }

            public ActionResult sizeproduct(int? id)
        {

            var result = _size.Table.Where(p => p.ProductId == id).ToList();
            ViewBag.ProductId = id;

            return View(result);
        }


        [HttpPost]
        public async Task<ActionResult> saveSizeForProduct(ProductSize row)
        {
            if (ModelState.IsValid)
            {
                _size.Add(row);

                return Json(new { issave = true, message = "ثبت باموفقیت انجام شد" });
            }

            return Json(new { issave = false, message = "error occuere" });
        }


        [HttpPost]
        public async Task<ActionResult> savePriceForProduct(PIP row)
        {
            if (ModelState.IsValid)
            {
                _pip.Add(row);

                return Json(new { issave = true, message = "ثبت باموفقیت انجام شد" });
            }

            return Json(new { issave = false, message = "error occuere" });
        }



        public ActionResult deletesize(int? id)
        {

            var result = _size.Table.Where(p => p.Id == id).FirstOrDefault();

            _size.Delete(result);

            return RedirectToAction("sizeproduct", new { id = result.ProductId });
        }


        public ActionResult deletepic(int? id)
        {

            var result = _pic.Table.Where(p => p.Id == id).FirstOrDefault();

            _pic.Delete(result);

            return RedirectToAction("picproduct", new { id = result.productId });
        }



        [HttpPost]
        public async Task<ActionResult> saveColorForProduct(ProductColor row)
        {
            if (ModelState.IsValid)
            {
                _color.Add(row);
               
                return Json(new { issave = true, message = "ثبت باموفقیت انجام شد" });
            }

            return Json(new { issave = false, message = "error occuere" });
        }


        public ActionResult deletecolor(int? id)
        {

            var result = _color.Table.Where(p => p.Id == id).FirstOrDefault();

            _color.Delete(result);
            
            return RedirectToAction("colorproduct",new { id= result.ProductId});
        }


        public ActionResult deleteproduct(int? id)
        {
            if (id != null)
            {
               var prdpic= _pic.Table.Where(p => p.productId == id).ToList();
                _pic.DeleteRange(prdpic);

                var prdpip = _pip.Table.Where(p => p.ProductId == id).ToList();
                _pip.DeleteRange(prdpip);

                var prdcolor = _color.Table.Where(p => p.ProductId == id).ToList();
                _color.DeleteRange(prdcolor);

                var prdsize = _size.Table.Where(p => p.ProductId == id).ToList();
                _size.DeleteRange(prdsize);

                var result = _product.Table.Where(p => p.Id == id).FirstOrDefault();
                if (result != null)
                {
                    _product.Delete(result);
                }
            }
            return RedirectToAction("ProductList");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
       // [ValidateInput(false)]
        public ActionResult CreateProduct(Product model)
        {
            var result = _product.Table.Where(p => p.Id == model.Id).FirstOrDefault();
            if (ModelState.IsValid)
            {
                if (result != null)
                {
                    result.productName = model.productName;
                    result.IsSpecial = model.IsSpecial;
                    result.productDescription = model.productDescription;
                    result.productSummery = model.productSummery;
                    _product.Update(result);
                }
                else
                {
                    _product.Add(model);
                }



                return RedirectToAction("ProductList");
            }
            return View(model);


        }
        public ActionResult GroupList()
        {
            var testTable = _group.Table.ToList();


            return View(testTable);
        }
        public ActionResult deletegroup(int? id)
        { 
            if (id != null)
            {
                var result=_group.Table.Where(p=>p.Id==id).FirstOrDefault();
                if (result != null)
                {
                    _group.Delete(result);
                }
            }
            return RedirectToAction("GroupList");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateGroup(ProductCategory model)
        {
            var result = _group.Table.Where(p => p.Id == model.Id).FirstOrDefault();
            if (ModelState.IsValid)
            {
                if (result != null)
                {
                    result.categoryName = model.categoryName;
                    _group.Update(result);
                }
                else
                {
                    _group.Add(model);
                }



                return RedirectToAction("GroupList");
            }
            return View(model);


        }
        public ActionResult CreateGroup(int?  id)
        {
            ProductCategory cat = new();
            if (id != null)
            {
                var result= _group.Table.Where(p=> p.Id==id).FirstOrDefault();
                if (result != null)
                {
                    cat = result;
                }
            }
           //ViewBag.createproduct = _group.ToList();
            return View(cat);
        }





        // Get: dashbordController/Create
        //public ActionResult CreateProduct()
        //{

        //    return View();
        //}
        // POST: dashbordController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public IActionResult CreateProduct(Product product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _product.Add(product);
               
                
        //        return RedirectToAction(nameof(Index));
        //    }

        //    return View(product);
        //}

     

        // GET: dashbordController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //// POST: dashbordController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit(int id, Product model1)
        //{

        //    var test = _product.Table.FirstOrDefault(model1 => model1.Id == id);
        //    if (test == null)
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

        //    return View();

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
