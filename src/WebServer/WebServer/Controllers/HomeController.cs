using System;
using System.Collections.Generic;
using System.Diagnostics;
using Castle.Core.Internal;
using HotelServicesNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebServer.Models;

using static WebServer.Helpers.AuthorizationHelper;

namespace WebServer.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUsersContainer _usersContainer;
        private readonly IServiceInfoContainer _serviceInfoContainer;

        public HomeController(IServiceInfoContainer serviceInfoContainer, IUsersContainer usersContainer)
        {
            _usersContainer = usersContainer;
            _serviceInfoContainer = serviceInfoContainer;
        }

        public IActionResult Index(string message)
        {
            ViewData["message"] = message;
            ViewData["roleName"] = HttpContext.Session.GetString("roleName");
            ViewData["login"] = HttpContext.Session.GetString("login");
            return View();
        }

        public IActionResult Services()
        {
            var services = _serviceInfoContainer.GetAvailableServices();
            ViewData["columns"] = 2;
            ViewData["roleName"] = HttpContext.Session.GetString("roleName");
            ViewData["login"] = HttpContext.Session.GetString("login");
            return View(services as List<ServiceInfo>);
        }

        public IActionResult Authorization(string message)
        {
            ViewData["message"] = message;
            ViewData["roleName"] = HttpContext.Session.GetString("roleName");
            ViewData["login"] = HttpContext.Session.GetString("login");
            ViewData["where"] = Request.Query["where"];
            return View();
        }

        public object Login()
        {
            var login = Request.Form["login"];
            var password = Request.Form["password"];

            var user = _usersContainer.GetUserByLogin(login);
            if (user == null)
            {
                return Json(new { message = "Неверный логин или пароль"});
            }

            if (user.Password != password)
            {
                return Json(new { message = "Неверный логин или пароль" });
            }

            AuthorizeInDb(HttpContext.Session.Id, user.Id);

            HttpContext.Session.SetString("roleId", user.Role.Id);
            HttpContext.Session.SetString("roleName", user.Role.Name);
            HttpContext.Session.SetString("login", user.Login);
            HttpContext.Session.SetString("fio", user.Fio);
            HttpContext.Session.SetString("userId", user.Id);
            HttpContext.Session.SetString("role", user.Role.Name);

            return Json(new { ok = true, login = user.Login, role = user.Role.Name });
        }

        public object Logout()
        {
            var userId = HttpContext.Session.GetString("userId");
            if (userId != null)
            {
                ClearOldEntities(userId);
            }

            HttpContext.Session.Clear();
            Response.Cookies.Delete("sessionId");
            Response.Cookies.Delete("login");
            Response.Cookies.Delete("roleId");

            return RedirectToAction("Index", "Home");
        }
                
        public object RegAction()
        {
            var login = Request.Form["login"];
            var password = Request.Form["password"];
            var fio = Request.Form["fio"];
            var roleName = Request.Form["role"];

            if (fio.IsNullOrEmpty() ||
                login.IsNullOrEmpty() ||
                password.IsNullOrEmpty() ||
                roleName.IsNullOrEmpty())
            {
                return Json(new { message = "Некорректные параметры" });
            }

            if (IsExistsLogin(login))
            {
                return Json(new { message = "Логин уже существует" });
            }

            AddUserToDb(fio, login, password, roleName);

            return Json(new { ok = true });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            ViewData["roleName"] = HttpContext.Session.GetString("roleName");
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
