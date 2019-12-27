using System;
using System.Globalization;
using Invento.Helpers;
using Invento.Providers.List;
using Invento.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Invento.Controllers
{
    [Route("Purchase")]
    public class PurchaseController : Controller
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
            var viewModel = new PurchaseViewModel
            {
                UpdatedDateTime = $"{ReadDateTime.ReadDate(dateTime)} {ReadDateTime.ReadTime(dateTime)}",
                Purchases = JsonConvert.SerializeObject(ListProvider.LoadPurchases())
            };
            return PartialView("_Purchase", viewModel);
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            return PartialView("_CreateModal");
        }
    }
}