using System;
using System.Globalization;
using System.Reflection;
using Invento.Helpers;
using Invento.Providers.List;
using Invento.ViewModels;
using Microsoft.AspNetCore.Mvc;

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
            var dateTime = DateTime.Now.ToString(CultureInfo.InvariantCulture);
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
    }
}