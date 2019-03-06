using System;
using System.Collections.Generic;
using System.Globalization;
using HotelServicesNetCore;
using Microsoft.AspNetCore.Http;
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
            var role = HttpContext.Session.GetString("role");
            if (role == null || role.ToLower() == "клиент" || 
                !IsAuthorizedInDb(HttpContext.Session.Id))
            {
                return RedirectToAction("Authorization", "Home");
            }

            var users = _usersContainer.GetUsers();
            ViewData["roleName"] = HttpContext.Session.GetString("roleName");
            ViewData["login"] = HttpContext.Session.GetString("login");
            return View(users as List<User>);
        }

        public IActionResult Orders()
        {
            var role = HttpContext.Session.GetString("role");
            if (role == null || role.ToLower() == "клиент" ||
                !IsAuthorizedInDb(HttpContext.Session.Id))
            {
                return RedirectToAction("Authorization", "Home");
            }

            DateTime? start;
            DateTime? end;

            var parsed = DateTime.TryParseExact(Request.Query["start"], "dd.MM.yyyy", 
                CultureInfo.CurrentCulture, DateTimeStyles.None, out var date);

            if (parsed)
            {
                start = date;
            }
            else
            {
                start = null;
            }

            parsed = DateTime.TryParseExact(Request.Query["end"], "dd.MM.yyyy",
                CultureInfo.CurrentCulture, DateTimeStyles.None, out date);

            if (parsed)
            {
                end = date;
            }
            else
            {
                end = null;
            }

            var user = _usersContainer.GetUserByLogin(Request.Query["user"]);
            var orders = _ordersContainer.GetOrders(user, from: start, to: end);

            ViewData["roleName"] = HttpContext.Session.GetString("roleName");
            ViewData["login"] = HttpContext.Session.GetString("login");
            return View(orders as List<Order>);
        } 
    }
}