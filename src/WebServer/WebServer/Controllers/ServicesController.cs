using System;
using System.Linq;
using Castle.Core.Internal;
using HotelServicesNetCore;
using Microsoft.AspNetCore.Http;
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
            var role = HttpContext.Session.GetString("role");
            if (role == null || role.ToLower() != "клиент" ||
                !IsAuthorizedInDb(HttpContext.Session.Id))
            {
                return RedirectToAction("Authorization", "Home");
            }

            var service = _serviceInfoContainer.GetServiceInfoById(id);
            if (service == null)
            {
                return RedirectToAction("Services", "Home", new { message = "Услуга не существует" });
            }

            ViewData["login"] = HttpContext.Session.GetString("login");
            return View(service);
        }

        public IActionResult Change(string id)
        {
            var role = HttpContext.Session.GetString("role");
            if (role == null || role.ToLower() != "управляющий" ||
                !IsAuthorizedInDb(HttpContext.Session.Id))
            {
                return RedirectToAction("Authorization", "Home");
            }

            var service = _serviceInfoContainer.GetServiceInfoById(id);
            if (service == null)
            {
                return RedirectToAction("Services", "Home", new { message = "Услуга не существует" });
            }

            ViewData["roleName"] = HttpContext.Session.GetString("roleName");
            ViewData["login"] = HttpContext.Session.GetString("login");
            return View(service);
        }

        public IActionResult Basket()
        {
            var role = HttpContext.Session.GetString("role");
            if (role == null || role.ToLower() != "клиент" ||
                !IsAuthorizedInDb(HttpContext.Session.Id))
            {
                return RedirectToAction("Authorization", "Home");
            }

            var user = _usersContainer.GetUserById(HttpContext.Session.GetString("userId"));
            if (user == null)
            {
                RedirectToAction("Authorization", "Home");
            }

            var ordersParams = _ordersContainer
                .GetOrders(user, unpaid: true, paid: false)
                .Select(o => new OrderParams
                {
                    Id = o.Id,
                    Cost = o.Cost,
                    ImgSrc = o.Service.ImgSrc,
                    Measurement = o.Service.Measurement,
                    ServiceName = o.Service.Name,
                    Units = o.Units
                })
                .ToList();

            ViewData["columns"] = 2;
            ViewData["role"] = HttpContext.Session.GetString("role");
            ViewData["login"] = HttpContext.Session.GetString("login");
            return View(ordersParams);
        }

        public object ChangeAction()
        {
            var role = HttpContext.Session.GetString("role");
            if (role == null || role.ToLower() != "управляющий" ||
                !IsAuthorizedInDb(HttpContext.Session.Id))
            {
                return Json(new { message = "NO_AUTHORIZED" });
            }

            var id = Request.Form["id"];

            var service = _serviceInfoContainer.GetServiceInfoById(id);
            if (service == null || service.IsDeprecated)
            {
                return Json(new { message = "" });
            }

            var name = Request.Form["name"].ToString();
            var measurement = Request.Form["measurement"].ToString();
            var costPerUnit = uint.Parse(Request.Form["costPerUnit"]); //проверить на знак

            if (name.IsNullOrEmpty() || measurement.IsNullOrEmpty())
            {
                return Json(new { message = "Некорректные параметры услуги" });
            }

            var newService = new ServiceInfo
            {
                Id = Guid.NewGuid().ToString(),
                CostPerUnit = costPerUnit,
                ImgSrc = service.ImgSrc,
                IsDeprecated = false,
                Measurement = measurement,
                Name = name
            };

            _servicesOperations.ChangeServiceInfo(service, newService);

            ViewData["login"] = HttpContext.Session.GetString("login");
            return Json(new { ok = true });
        }

        public object OrderAction()
        {
            var role = HttpContext.Session.GetString("role");
            if (role == null || role.ToLower() != "клиент" ||
                !IsAuthorizedInDb(HttpContext.Session.Id))
            {
                return Json(new {message = "NO_AUTHORIZED"});
            }

            var service = _serviceInfoContainer.GetServiceInfoById(Request.Form["id"]);
            if (service == null)
            {
                return Json(new { message = "" });
            }

            var userId = HttpContext.Session.GetString("userId");
            var user = _usersContainer.GetUserById(userId);
            if (user == null)
            {
                return Json(new { message = "NO_AUTHORIZED" });
            }

            var units = uint.Parse(Request.Form["units"]);
            _usersOperations.OrderService(user, service.Name, units);

            return Json(new { ok = true });
        }

        public object Buy()
        {
            var role = HttpContext.Session.GetString("role");
            if (role == null || role.ToLower() != "клиент" ||
                !IsAuthorizedInDb(HttpContext.Session.Id))
            {
                return Json(new { message = "NO_AUTHORIZED" });
            }

            var order = _ordersContainer.GetOrderById(Request.Form["id"]);
            if (order == null)
            {
                return Json(new { message = "" });
            }

            _usersOperations.PayService(order.User, order.Id);
            return Json(new { ok = true });
        }

        public object Cancel(string id)
        {
            var role = HttpContext.Session.GetString("role");
            if (role == null || role.ToLower() != "клиент" ||
                !IsAuthorizedInDb(HttpContext.Session.Id))
            {
                return Json(new { message = "NO_AUTHORIZED" });
            }

            var order = _ordersContainer.GetOrderById(id);
            if (order == null)
            {
                return Json(new { message = "" });
            }

            _usersOperations.CancelService(order.User, order.Id);
            return Json(new { ok = true });
        }

        public class OrderParams
        {
            public string Id { get; set; }
            public string ServiceName { get; set; }
            public string ImgSrc { get; set; }
            public decimal Cost { get; set; }
            public uint Units { get; set; }
            public string Measurement { get; set; }
        }
    }
}