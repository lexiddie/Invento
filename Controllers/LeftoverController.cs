using System;
using System.Globalization;
using Invento.Helpers;
using Invento.Providers.List;
using Invento.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Invento.Controllers
{
    [Route("Leftover")]
    public class LeftoverController : Controller
    {
        private static readonly ListProvider ListProvider = new ListProvider();

        [HttpGet]
        [Route("Index")]
        public IActionResult Index()
        {
            return PartialView("Index");
        }

        [HttpGet]
        [Route("List")]
        public IActionResult List()
        {
            var dateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            var viewModel = new LeftoverViewModel
            {
                UpdatedDateTime = $"{ReadDateTime.ReadDate(dateTime)} {ReadDateTime.ReadTime(dateTime)}",
                Leftovers = JsonConvert.SerializeObject(ListProvider.LoadLeftovers())
            };
            return PartialView("_Leftover", viewModel);
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            return PartialView("_CreateModal");
        }
        
        [HttpPost]
        [Route("Delete")]
        public IActionResult Delete(string id)
        {
            ListProvider.VoidLeftover(id);
            return Json(new { isSuccess = true} as dynamic);
        }
    }
}
