using Microsoft.AspNetCore.Mvc;

namespace Invento.Controllers
{
    public class MeasurementController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return PartialView("Index");
        }
    }
}