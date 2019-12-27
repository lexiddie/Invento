using System;
using System.Globalization;
using Invento.Helpers;
using Invento.Providers.List;
using Invento.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Invento.Controllers
{
    [Route("Usage")]
    public class UsageController : Controller
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
            var viewModel = new UsageViewModel
            {
                UpdatedDateTime = $"{ReadDateTime.ReadDate(dateTime)} {ReadDateTime.ReadTime(dateTime)}",
                Usages = JsonConvert.SerializeObject(ListProvider.LoadUsages())
            };
            return PartialView("_Usage", viewModel);
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            return PartialView("_CreateModal");
        }
    }
}