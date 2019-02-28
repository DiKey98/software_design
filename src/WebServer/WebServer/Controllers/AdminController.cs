using Microsoft.AspNetCore.Mvc;

namespace WebServer.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Users()
        {
            return View();
        }

        public IActionResult Orders()
        {
            return View();
        }

        public IActionResult Services()
        {
            return View();
        }
    }
}