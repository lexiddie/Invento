using Microsoft.AspNetCore.Mvc;

namespace Invento.Controllers
{
    public class UserController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return PartialView("Index");
        }
    }
}