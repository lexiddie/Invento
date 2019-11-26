using Microsoft.AspNetCore.Mvc;

namespace Invento.Controllers
{
    public class UsageController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}