using Microsoft.AspNetCore.Mvc;

namespace Invento.Controllers
{
    public class ProductController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return PartialView("Index");
        }
    }
}