﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Invento.Models;
using Firebase.Database;    
using Firebase.Database.Query;
using Microsoft.Extensions.Caching.Memory;

namespace Invento.Controllers
{
    public class HomeController : Controller
    {
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
            Firebase();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
        
        public IActionResult ErrorPage()
        {
            return PartialView("ErrorPage");
        }

        public async Task Firebase()
        {
            var firebase = new FirebaseClient("https://invento-e28df.firebaseio.com/");
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