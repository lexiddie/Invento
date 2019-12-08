using Microsoft.AspNetCore.Mvc;

namespace Invento.Controllers
{
    public class LeftoverController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}