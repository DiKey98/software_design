using HotelServicesNetCore;
using Microsoft.AspNetCore.Mvc;

namespace WebServer.Controllers
{
    public class ServicesController : Controller
    {
        private readonly IUsersOperations _usersOperations;
        private readonly IServiceInfoContainer _serviceInfoContainer;
        private readonly IUsersContainer _usersContainer;

        public ServicesController(IServiceInfoContainer serviceInfoContainer, 
            IUsersContainer usersContainer, IUsersOperations usersOperations)
        {
            _usersOperations = usersOperations;
            _serviceInfoContainer = serviceInfoContainer;
            _usersContainer = usersContainer;
        }

        public IActionResult Service(string id)
        {
            return View();
        }

        public IActionResult Change(string id)
        {
            return View();
        }

        [HttpPost]
        public void Order(string id)
        {
            //TODO проверка авторизации

            var service = _serviceInfoContainer.GetServiceInfoById(id);
            if (service == null)
            {
                Response.Redirect("{controller = Services}/{action = Order}");
                return;
            }

            var user = _usersContainer.GetUserById(Request.Form["userId"]);
            if (user == null)
            {
                Response.Redirect("{controller = Home}/{action = Authorization}");
                return;
            }

            var units = uint.Parse(Request.Form["units"]);

            _usersOperations.OrderService(user, service.Name, units);
            Response.Redirect("{controller = Services}/{action = Order}");
        }

        [HttpPost]
        public IActionResult Buy(string id)
        {
            return null;
        }

        [HttpPost]
        public IActionResult Cancel(string id)
        {
            return null;
        }
    }
}