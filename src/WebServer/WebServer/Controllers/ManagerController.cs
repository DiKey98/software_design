using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Castle.Core.Internal;
using HotelServicesNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using static WebServer.Helpers.AuthorizationHelper;

namespace WebServer.Controllers
{
    public class ManagerController : Controller
    {
        private readonly IOrdersContainer _ordersContainer;

        public ManagerController(IOrdersContainer ordersContainer)
        {
            _ordersContainer = ordersContainer;
        }

        public IActionResult UsersActivity()
        {
            var role = HttpContext.Session.GetString("role");
            if (role == null || role.ToLower() != "управляющий" ||
                !IsAuthorizedInDb(HttpContext.Session.Id))
            {
                return RedirectToAction("Authorization", "Home");
            }

            if (Request.Method == "GET")
            {
                var ords = _ordersContainer.GetOrders(null, from: DateTime.Today, to: DateTime.Today);

                var st = ords.GroupBy(o => o.User)
               .Select(group => new UsersActivityStatistics
               {
                   UserName = group.Key.Fio,
                   OrdersCount = group.Count(),
                   PaidOrdersCount = group.Count(s => s.IsPaid),
                   UnPaidOrdersCount = group.Count(s => !s.IsPaid),
                   OrdersCost = group.Sum(s => s.Cost)
               })
               .OrderBy(x => x.UserName).ToList();

                ViewData["roleName"] = HttpContext.Session.GetString("roleName");
                ViewData["login"] = HttpContext.Session.GetString("login");
                return View(st);
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

            var statistics = orders.GroupBy(o => o.User)
                .Select(group => new UsersActivityStatistics
                {
                    UserName = group.Key.Fio,
                    OrdersCount = group.Count(),
                    PaidOrdersCount = group.Count(s => s.IsPaid),
                    UnPaidOrdersCount = group.Count(s => !s.IsPaid),
                    OrdersCost = group.Sum(s => s.Cost)
                })
                .OrderBy(x => x.UserName).ToList();

            ViewData["roleName"] = HttpContext.Session.GetString("roleName");
            ViewData["login"] = HttpContext.Session.GetString("login");
            return Json(statistics);
        }

        public IActionResult RegManager()
        {
            var role = HttpContext.Session.GetString("role");
            if (role == null || role.ToLower() != "управляющий" ||
                !IsAuthorizedInDb(HttpContext.Session.Id))
            {
                return RedirectToAction("Authorization", "Home");
            }

            ViewData["roleName"] = HttpContext.Session.GetString("roleName");
            ViewData["login"] = HttpContext.Session.GetString("login");
            return View();
        }

        public class UsersActivityStatistics
        {
            public string UserName { get; set; }
            public int OrdersCount { get; set; }
            public int PaidOrdersCount { get; set; } 
            public int UnPaidOrdersCount { get; set; }
            public decimal OrdersCost { get; set; }
        }
    }
}