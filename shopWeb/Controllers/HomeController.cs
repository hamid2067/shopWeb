using Common.Utility;
using Data;
using Data.Repositories;
using Entities;
using Entities.Basket;
using Entities.Product;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using shopWeb.Models;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using RequestParameters = shopWeb.Models.RequestParameters;

namespace shopWeb.Controllers
{
    public class HomeController : Controller
    {

        private readonly IUserRepository userRepository;
        private readonly IRepository<Product> _product;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly SignInManager<User> signInManager;
        private readonly IRepository<PIP> _pip;
        private readonly IRepository<Basket> _basket;


        string merchant = "c6a58e82-608d-448d-91b0-1e70989626";
        string amount = "1100";
        string authority;
        string description = "خرید تستی ";
        string callbackurl = "http://localhost:2812/Home/VerifyByHttpClient";




        public HomeController(IRepository<Basket> basket, IRepository<PIP> pip, IRepository<Product> product,IUserRepository userRepository,
            UserManager<User> userManager,
            RoleManager<Role> roleManager, SignInManager<User> signInManager)
        {
            this.userRepository = userRepository;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
            this._product = product;
            this._pip = pip;
            this._basket = basket;

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

     var prd = _product.Table.Include(p => p.pips).Include(p=>p.productCategory).Include(p=>p.colors).Include(p=>p.sizes).Include(p => p.Images).Where(p => p.Id == id).FirstOrDefault();

            return View(prd);
        }

        [HttpPost]
        public async Task<ActionResult> savePrdToBasket(prdbasket prd)
        {
            if(!User.Identity.IsAuthenticated)
            {
                return Json(new
                {
                    error=true,
                    msg="ابتدا لاگین کنید",
                });
            }
            
            var prdselect=_product.Table.Where(p=>p.Id==prd.prdid).FirstOrDefault();
            var price = _pip.Table.Where(p => p.ProductId == prd.prdid &&
             p.colorId == prd.colorId && p.sizeId == prd.sizeId).First().Price;

            if (prdselect !=null)
            {
                Basket me = new();
                me.Number = prd.prdnumbers;
                me.productId = prdselect.Id;
                me.sizeId = prd.sizeId;
                me.colorId = prd.colorId;
                me.Price = price;
                me.userId =int.Parse( User.Identity.GetUserId()) ;
                _basket.Add(me);

            }

            return Json(new
            {
                error = false,
                msg = "کالا با موفقیت افزوده شد",
            });
        }


        [HttpPost]
        public async Task<ActionResult> calcProductPrice(pricePrd prd)
        {

            var product = _pip.Table.Where(p => p.ProductId == prd.prdId &&
            p.colorId == prd.selectColor && p.sizeId == prd.selectSize).FirstOrDefault();

            return Json(new { isExist = product!= null ?true:false,
                invoice= product?.invoice ,price= product?.Price });
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



        public IActionResult Payment()
        {


            try
            {
                RequestParameters Parameters = new RequestParameters(merchant, amount, description, callbackurl, "", "");



                //be dalil in ke metadata be sorate araye ast va do meghdare mobile va email dar metadata gharar mmigirad
                //shoma mitavanid in maghadir ra az kharidar begirid va set konid dar gheir in sorat khali ersal konid

                var client = new RestClient(URLs.requestUrl);

                RestSharp.Method method = RestSharp.Method.Post;

                var request = new RestRequest("", method);

                request.AddHeader("accept", "application/json");

                request.AddHeader("content-type", "application/json");

                request.AddJsonBody(Parameters);

                var requestresponse = client.ExecuteAsync(request);

                JObject jo = JObject.Parse(requestresponse.Result.Content);

                string errorscode = jo["errors"].ToString();

                JObject jodata = JObject.Parse(requestresponse.Result.Content);

                string dataauth = jodata["data"].ToString();


                if (dataauth != "[]")
                {


                    authority = jodata["data"]["authority"].ToString();

                    string gatewayUrl = URLs.gateWayUrl + authority;

                    return Redirect(gatewayUrl);

                }
                else
                {


                    return BadRequest("error " + errorscode);


                }


            }

            catch (Exception ex)
            {
                //    throw new Exception(ex.Message);


            }
            return null;
        }

        public async Task<IActionResult> PaymenBytHttpClient()
        {

            try
            {

                using (var client = new HttpClient())
                {
                    RequestParameters parameters = new RequestParameters(merchant, amount, description, callbackurl, "", "");

                    var json = JsonConvert.SerializeObject(parameters);

                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(URLs.requestUrl, content);

                    string responseBody = await response.Content.ReadAsStringAsync();

                    JObject jo = JObject.Parse(responseBody);
                    string errorscode = jo["errors"].ToString();

                    JObject jodata = JObject.Parse(responseBody);
                    string dataauth = jodata["data"].ToString();


                    if (dataauth != "[]")
                    {


                        authority = jodata["data"]["authority"].ToString();

                        string gatewayUrl = URLs.gateWayUrl + authority;

                        return Redirect(gatewayUrl);

                    }
                    else
                    {

                        return BadRequest("error " + errorscode);


                    }

                }


            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);


            }
            return NotFound();
        }

        public async Task<IActionResult> VerifyByHttpClient()
        {
            try
            {

                VerifyParameters parameters = new VerifyParameters();


                if (HttpContext.Request.Query["Authority"] != "")
                {
                    authority = HttpContext.Request.Query["Authority"];
                }

                parameters.authority = authority;

                parameters.amount = amount;

                parameters.merchant_id = merchant;


                using (HttpClient client = new HttpClient())
                {

                    var json = JsonConvert.SerializeObject(parameters);

                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(URLs.verifyUrl, content);

                    string responseBody = await response.Content.ReadAsStringAsync();

                    JObject jodata = JObject.Parse(responseBody);

                    string data = jodata["data"].ToString();

                    JObject jo = JObject.Parse(responseBody);

                    string errors = jo["errors"].ToString();

                    if (data != "[]")
                    {
                        string refid = jodata["data"]["ref_id"].ToString();

                        ViewBag.code = refid;

                        return View();
                    }
                    else if (errors != "[]")
                    {

                        string errorscode = jo["errors"]["code"].ToString();

                        return BadRequest($"error code {errorscode}");

                    }
                }



            }
            catch (Exception ex)
            {

                throw ex;
            }
            return NotFound();
        }


        public IActionResult VerifyPayment()
        {

            // string authorityverify;

            try
            {
                VerifyParameters parameters = new VerifyParameters();


                if (HttpContext.Request.Query["Authority"] != "")
                {
                    authority = HttpContext.Request.Query["Authority"];
                }

                parameters.authority = authority;
                parameters.amount = amount;
                parameters.merchant_id = merchant;


                var client = new RestClient(URLs.verifyUrl);
                RestSharp.Method method = RestSharp.Method.Post;
                var request = new RestRequest("", method);

                request.AddHeader("accept", "application/json");

                request.AddHeader("content-type", "application/json");
                request.AddJsonBody(parameters);

                var response = client.ExecuteAsync(request);


                JObject jodata = JObject.Parse(response.Result.Content);

                string data = jodata["data"].ToString();

                JObject jo = JObject.Parse(response.Result.Content);

                string errors = jo["errors"].ToString();

                if (data != "[]")
                {
                    string refid = jodata["data"]["ref_id"].ToString();

                    ViewBag.code = refid;

                    return View();
                }
                else if (errors != "[]")
                {

                    string errorscode = jo["errors"]["code"].ToString();

                    return BadRequest($"error code {errorscode}");

                }


            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return NotFound();
        }

    }
}