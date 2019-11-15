using Microsoft.AspNetCore.Mvc;

namespace Invento.Controllers
{
    public class PurchaseController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return PartialView("Index");
        }
    }
}