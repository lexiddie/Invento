using Microsoft.AspNetCore.Mvc;

namespace Invento.Controllers
{
    public class LoginController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return PartialView("Index");
        }
    }
}