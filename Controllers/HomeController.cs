using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Invento.Models;
using Firebase.Database;
using Firebase.Database.Query;
using Invento.Helpers;
using Invento.Providers.API;
using Invento.Providers.List;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace Invento.Controllers
{
    public class HomeController : Controller
    {
        private new const string Url = "https://invento-e28df.firebaseio.com/";
        
        private static readonly ApiProvider ApiProvider = new ApiProvider();
        private static readonly ListProvider ListProvider = new ListProvider();

        private readonly MemoryCacheEntryOptions _options = new MemoryCacheEntryOptions();
        private readonly MemoryCacheEntryOptions _entryOptions = new MemoryCacheEntryOptions();
        private readonly IMemoryCache _cache;

        public HomeController(IMemoryCache cache)
        {
            _options.AbsoluteExpiration = DateTime.Now.AddMinutes(30);
            _options.SlidingExpiration = TimeSpan.FromMinutes(30);
            _entryOptions.SetPriority(CacheItemPriority.NeverRemove);
            _cache = cache;
        }
        public IActionResult Index()
        {
//            Console.WriteLine(ApiProvider.ApiCreateMeasurement("-LuQjw-lLKluye7vLkTb", "Kilogram", "Kg", "This is kg", true));
//            Console.WriteLine(ApiProvider.ApiMeasurements().Count);
            Console.WriteLine("Check Return");
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

        public IActionResult ErrorPage()
        {
            return PartialView("ErrorPage");
        }

        public IActionResult CheckLogin()
        {
            if (!_cache.Get<bool>("isLogin"))
            {
                _cache.Set("isLogin", false, _entryOptions);
            }
            var current = new { isLogin = _cache.Get<bool>("isLogin") } as dynamic;
            return Json(current);
        }

        public IActionResult VerifyLogin(string username, string password)
        {
            if (username != "admin" || password != "123") return Json(new { isSuccess = false } as dynamic);
            _cache.Set("isLogin", true, _entryOptions);
            _cache.Set("loginUsername", "admin", _entryOptions);
            return Json(new { isSuccess = true, username = "admin" } as dynamic);
        }

        public IActionResult Logout()
        {
            _cache.Set("isLogin", false, _entryOptions);
            return Json(new { isSuccess = true} as dynamic);
        }

        public IActionResult CheckProduct(string name, string code)
        {
            return Json(new { isValid = ListProvider.CheckProduct(name, code)} as dynamic);
        }

        public IActionResult CurrentDateTime()
        {
            return Json(new { dateTime = DateTime.Now.ToString(CultureInfo.InvariantCulture)} as dynamic );
        }

        public IActionResult CreateMeasurement(string id, string name, string abbreviation, string description, bool status)
        {
            return Json(ApiProvider.ApiCreateMeasurement(id, name, abbreviation, description, status));
        }

        public IActionResult CompletionCategories()
        {
            return Json(ListProvider.LoadCategories());
        }
        
        public IActionResult CompletionMeasurements()
        {
            return Json(ListProvider.LoadMeasurements());
        }
        
        public IActionResult CompletionProducts()
        {
            return Json(ListProvider.LoadProducts());
        }

        public IActionResult CompletionSuppliers()
        {
            return Json(ListProvider.LoadSuppliers());
        }

        public IActionResult CompletionCountries()
        {
            var json = ReadJson.LoadJson("./data/json/Countries.json");
            return Json(JsonConvert.DeserializeObject<List<Country>>(json));
        }

        public IActionResult GeneratePurchase()
        {
            return Json(new { purchaseCode = ListProvider.GeneratePurchase()} as dynamic);
        }
        
        public IActionResult GenerateUsage()
        {
            return Json(new { usageCode = ListProvider.GenerateUsage()} as dynamic);
        }
        
        public IActionResult GenerateLeftover()
        {
            return Json(new { leftoverCode = ListProvider.GenerateLeftover()} as dynamic);
        }

        public IActionResult LoadAvailable(string productId)
        {
            return Json(new {available = ListProvider.LoadAvailable(productId)});
        }

        public async Task Firebase()
        {
            var firebase = new FirebaseClient(Url);
            //            var data = await firebase
            //                .Child("User")
            //                .OrderByKey()
            //                .StartAt("pterodactyl")
            //                .LimitToFirst(2)
            //                .OnceAsync<Dinosaur>();

            var data = await firebase.Child("me").OrderByKey().OnceAsync<dynamic>();
            Console.WriteLine(data.ToString());
            Console.WriteLine(data.Count);

            foreach (var item in data)
            {
                Console.WriteLine($"{item.Key} {item.Object.ToString()}");
            }
        }
    }
}