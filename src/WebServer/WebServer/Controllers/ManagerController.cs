using Microsoft.AspNetCore.Mvc;

namespace WebServer.Controllers
{
    public class ManagerController : Controller
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