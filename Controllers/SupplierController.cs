using Microsoft.AspNetCore.Mvc;

namespace Invento.Controllers
{
    public class SupplierController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return PartialView("Index");
        }
    }
}