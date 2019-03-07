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

        public object Orders()
        {
            var role = HttpContext.Session.GetString("role");
            if (role == null || role.ToLower() == "клиент" ||
                !IsAuthorizedInDb(HttpContext.Session.Id))
            {
                return RedirectToAction("Authorization", "Home");
            }

            if (Request.Method == "GET")
            {
                var ords = _ordersContainer.GetOrders(null, from: DateTime.Today, to: DateTime.Today);
                ViewData["roleName"] = HttpContext.Session.GetString("roleName");
                ViewData["login"] = HttpContext.Session.GetString("login");
                return View(ords as List<Order>);
            }

            DateTime? start;
            DateTime? end;

            var parsed = DateTime.TryParseExact(Request.Form["start"], "dd.MM.yyyy", 
                CultureInfo.CurrentCulture, DateTimeStyles.None, out var date);

            if (parsed)
            {
                start = date;
            }
            else
            {
                start = null;
            }

            parsed = DateTime.TryParseExact(Request.Form["end"], "dd.MM.yyyy",
                CultureInfo.CurrentCulture, DateTimeStyles.None, out date);

            if (parsed)
            {
                end = date;
            }
            else
            {
                end = null;
            }

            var orders = _ordersContainer.GetOrders(null, from: start, to: end);

            ViewData["roleName"] = HttpContext.Session.GetString("roleName");
            ViewData["login"] = HttpContext.Session.GetString("login");
            return Json(orders);
            //return View(orders as List<Order>);
        } 
    }
}