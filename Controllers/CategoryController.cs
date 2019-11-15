using Microsoft.AspNetCore.Mvc;

namespace Invento.Controllers
{
    public class CategoryController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return PartialView("Index");
        }
    }
}