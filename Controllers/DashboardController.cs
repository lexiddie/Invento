using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Invento.Helpers;
using Invento.Models;
using Invento.Providers.List;
using Invento.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Invento.Controllers
{
    [Route("Dashboard")]
    public class DashboardController : Controller
    {
        private static readonly ListProvider ListProvider = new ListProvider();

        [HttpGet]
        [Route("Index")]
        public IActionResult Index()
        {
            return PartialView("Index");
        }

        [HttpGet]
        [Route("Dashboard")]
        public IActionResult Dashboard()
        {
            var dateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            var viewModel = new DashboardViewModel
            {
                UpdatedDateTime = $"{ReadDateTime.ReadDate(dateTime)} {ReadDateTime.ReadTime(dateTime)}",
                Measurement = ListProvider.MeasurementQuantity(),
                Supplier = ListProvider.SupplierQuantity(),
                Category = ListProvider.CategoryQuantity(),
                Product = ListProvider.ProductQuantity(),
                Purchase = ListProvider.PurchaseQuantity(),
                Usage = ListProvider.UsageQuantity(),
                Leftover = ListProvider.LeftoverQuantity()
            };
            return PartialView("_Dashboard", viewModel);
        }

        [HttpGet]
        [Route("Visualization")]
        public IActionResult Visualization()
        {
            return PartialView("_Visualization");
        }

        [HttpPost]
        [Route("Inventories")]
        public IActionResult Inventories(string startDate, string toDate)
        {
            var viewModel = new InventoryViewModel
            {
                StartDate = startDate,
                ToDate = toDate,
                Inventories = JsonConvert.SerializeObject(ListProvider.LoadInventories(startDate, toDate))
            };
            return PartialView("_Inventory", viewModel);
        }
    }
}