using System;
using HotelServicesNetCore;
using Microsoft.AspNetCore.Mvc;

using static WebServer.Helpers.AuthorizationHelper;

namespace WebServer.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUsersContainer _usersContainer;
        private readonly IOrdersContainer _ordersContainer;

        public AdminController(IUsersContainer usersContainer, IOrdersContainer ordersContainer)
        {
            _usersContainer = usersContainer;
            _ordersContainer = ordersContainer;
        }

        public IActionResult Users()
        {
            if (!IsAuthorizedInDb(HttpContext.Session.Id))
            {
                return RedirectToAction("Authorization", "Home");
            }

            var users = _usersContainer.GetUsers();
            return View(users);
        }

        public IActionResult Orders()
        {
            if (!IsAuthorizedInDb(HttpContext.Session.Id))
            {
                return RedirectToAction("Authorization", "Home");
            }

            var orders = _ordersContainer.GetOrders(from: DateTime.Today, to: DateTime.Today);
            return View(orders);
        } 
    }
}