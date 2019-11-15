using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Invento.Models;
using Firebase.Database;    
using Firebase.Database.Query;

namespace Invento.Controllers
{
    public class HomeController : Controller
    {
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