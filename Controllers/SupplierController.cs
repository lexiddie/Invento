using System;
using System.Globalization;
using Invento.Helpers;
using Invento.Providers.List;
using Invento.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Invento.Controllers
{
    [Route("Supplier")]
    public class SupplierController : Controller
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
            var viewModel = new SupplierViewModel
            {
                UpdatedDateTime = $"{ReadDateTime.ReadDate(dateTime)} {ReadDateTime.ReadTime(dateTime)}",
                Suppliers = JsonConvert.SerializeObject(ListProvider.LoadSuppliers())
            };
            return PartialView("_Supplier", viewModel);
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            return PartialView("_CreateModal");
        }

        [HttpPost]
        [Route("Edit")]
        public IActionResult Edit(string Id)
        {
            return PartialView("_EditModal");
        }

        [HttpPost]
        [Route("Delete")]
        public IActionResult Delete(string Id)
        {
            return PartialView("_EditModal");
        }
    }
}