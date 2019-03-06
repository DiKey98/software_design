using System;
using Castle.Core.Internal;
using HotelServicesNetCore;
using Microsoft.AspNetCore.Mvc;

using static WebServer.Helpers.AuthorizationHelper;

namespace WebServer.Controllers
{
    public class ServicesController : Controller
    {
        private readonly IUsersOperations _usersOperations;
        private readonly IServiceInfoContainer _serviceInfoContainer;
        private readonly IUsersContainer _usersContainer;
        private readonly IServicesOperations _servicesOperations;
        private readonly IOrdersContainer _ordersContainer;

        public ServicesController(IServiceInfoContainer serviceInfoContainer, 
            IUsersContainer usersContainer, IUsersOperations usersOperations, 
            IServicesOperations servicesOperations, IOrdersContainer ordersContainer)
        {
            _usersOperations = usersOperations;
            _servicesOperations = servicesOperations;
            _ordersContainer = ordersContainer;
            _serviceInfoContainer = serviceInfoContainer;
            _usersContainer = usersContainer;
        }

        public IActionResult Order(string id)
        {
            //if (!IsAuthorizedInDb(HttpContext.Session.Id))
            //{
            //    return RedirectToAction("Authorization", "Home");
            //}

            var service = _serviceInfoContainer.GetServiceInfoById(id);
            if (service == null)
            {
                return RedirectToAction("Services", "Manager", new { message = "Услуга не существует" });
            }

            return View(service);
        }

        public IActionResult Change(string id)
        {
            //if (!IsAuthorizedInDb(HttpContext.Session.Id))
            //{
            //    return RedirectToAction("Authorization", "Home");
            //}

            //var service = _serviceInfoContainer.GetServiceInfoById(id);
            //if (service == null)
            //{
            //    return RedirectToAction("Services", "Manager", new { message = "Услуга не существует" });
            //}

            return View();
        }

        public void ChangeAction(string id)
        {
            //if (!IsAuthorizedInDb(HttpContext.Session.Id))
            //{
            //    return RedirectToAction("Authorization", "Home");
            //}

            //var service = _serviceInfoContainer.GetServiceInfoById(id);
            //if (service == null || service.IsDeprecated)
            //{
            //    return RedirectToAction("Services", "Manager", new { message = "Услуга не существует" });
            //}

            //var name = Request.Query["name"];
            //var measurement = Request.Query["measurement"];
            //var costPerUnit = uint.Parse(Request.Query["costPerUnit"]);

            //var name = "Пирамида";
            //var measurement = "мин.";
            //uint costPerUnit = 10000;

            //if (name.IsNullOrEmpty() || measurement.IsNullOrEmpty())
            //{
            //    return RedirectToAction("Change", "Services", new { message = "Некорретные параметры услуги", id = service.Id });
            //}

            //var newService = new ServiceInfo
            //{
            //    Id = Guid.NewGuid().ToString(),
            //    CostPerUnit = costPerUnit,
            //    ImgSrc = service.ImgSrc,
            //    IsDeprecated = false,
            //    Measurement = measurement,
            //    Name = name
            //};

            //_servicesOperations.ChangeServiceInfo(service, newService);

            //return RedirectToAction("Change", "Services", new {id = newService.Id});
        }

        public IActionResult OrderAction(string id)
        {
            if (!IsAuthorizedInDb(HttpContext.Session.Id))
            {
                return RedirectToAction("Authorization", "Home");
            }

            var service = _serviceInfoContainer.GetServiceInfoById(id);
            if (service == null)
            {
                return RedirectToAction("Order", "Services", new { message = "Услуга не существует" });
            }

            //var userId = Request.Query["userId"];
            var userId = "0c408d05-dc94-40b2-b257-098d1974ef65";
            var user = _usersContainer.GetUserById(userId);
            if (user == null)
            {
                return RedirectToAction("Authorization", "Home");
            }

            //var units = uint.Parse(Request.Query["units"]);
            uint units = 10;
            _usersOperations.OrderService(user, service.Name, units);

            return RedirectToAction("Order", "Services", new {id = service.Id});
        }

        public IActionResult Buy(string id)
        {
            if (!IsAuthorizedInDb(HttpContext.Session.Id))
            {
                return RedirectToAction("Authorization", "Home");
            }

            var order = _ordersContainer.GetOrderById(id);
            if (order == null)
            {
                return RedirectToAction("Order", "Services", new { message = "Услуга не существует" });
            }

            //var userId = Request.Query["userId"];
            var userId = "0c408d05-dc94-40b2-b257-098d1974ef65";
            var user = _usersContainer.GetUserById(userId);
            if (user == null)
            {
                return RedirectToAction("Authorization", "Home");
            }

            _usersOperations.PayService(user, order.Id);

            return RedirectToAction("Order", "Services", new { id = order.Id });
        }

        public IActionResult Cancel(string id)
        {
            if (!IsAuthorizedInDb(HttpContext.Session.Id))
            {
                return RedirectToAction("Authorization", "Home");
            }

            var order = _ordersContainer.GetOrderById(id);
            if (order == null)
            {
                return RedirectToAction("Order", "Services", new { message = "Услуга не существует" });
            }

            //var userId = Request.Query["userId"];
            var userId = "0c408d05-dc94-40b2-b257-098d1974ef65";
            var user = _usersContainer.GetUserById(userId);
            if (user == null)
            {
                return RedirectToAction("Authorization", "Home");
            }

            _usersOperations.CancelService(user, order.Id);

            return RedirectToAction("Order", "Services", new { id = order.Id });
        }
    }
}