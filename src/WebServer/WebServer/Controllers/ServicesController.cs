using Microsoft.AspNetCore.Mvc;

namespace WebServer.Controllers
{
    public class ServicesController : Controller
    {
        public IActionResult Service()
        {
            return View();
        }

        public IActionResult Change()
        {
            return View();
        }
    }
}