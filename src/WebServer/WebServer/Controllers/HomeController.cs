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
            return View(services as List<ServiceInfo>);
        }

        public IActionResult Registration(string message)
        {
            ViewData["message"] = message;
            ViewData["roleName"] = HttpContext.Session.GetString("roleName");
            return View();
        }

        public object Authorization(string message)
        {
            ViewData["message"] = message;
            ViewData["roleName"] = HttpContext.Session.GetString("roleName");
            return View();
        }

        public object Login()
        {
            var login = Request.Form["login"];
            var password = Request.Form["password"];

            //var login = "smr";
            //var password = "22222";

            var user = _usersContainer.GetUserByLogin(login);
            if (user == null)
            {
                //return RedirectToAction("Authorization", "Home", new { message = "Неверный логин или пароль" });
                return Json(new { message = "Неверный логин или пароль"});
            }

            if (user.Password != password)
            {
                //return RedirectToAction("Authorization", "Home", new { message = "Неверный логин или пароль" });
                return Json(new { message = "Неверный логин или пароль" });
            }

            AuthorizeInDb(HttpContext.Session.Id, user.Id);

            HttpContext.Session.SetString("roleId", user.Role.Id);
            HttpContext.Session.SetString("roleName", user.Role.Name);
            HttpContext.Session.SetString("login", user.Login);
            HttpContext.Session.SetString("fio", user.Fio);
            HttpContext.Session.SetString("userId", user.Id);
            HttpContext.Session.SetString("role", user.Role.Name);

            //Response.Cookies.Append("sessionId", HttpContext.Session.Id, new CookieOptions {MaxAge = TimeSpan.FromDays(10)});
            //Response.Cookies.Append("login", user.Login, new CookieOptions { MaxAge = TimeSpan.FromDays(10) });
            //Response.Cookies.Append("roleId", user.Role.Id, new CookieOptions { MaxAge = TimeSpan.FromDays(10) });

            return Json(new { ok = true, login = user.Login });
        }

        public IActionResult Logout()
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
                
        public void RegAction()
        {
            var login = Request.Form["login"];
            var password = Request.Form["password"];
            var fio = Request.Form["fio"];
            var roleName = Request.Form["role"];



            //if (fio.IsNullOrEmpty() || 
            //    login.IsNullOrEmpty() || 
            //    password.IsNullOrEmpty() || 
            //    roleName.IsNullOrEmpty())
            //{
            //    return RedirectToAction("Registration", "Home", new { message = "Некорректные параметры" });
            //}

            //if (IsExistsLogin(login))
            //{
            //    return RedirectToAction("Registration", "Home", new { message = "Логин уже существует" });
            //}

            //AddUserToDb(fio, login, password, roleName);

            //return RedirectToAction("Authorization", "Home", new { message = "Регистрация успешна" });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            ViewData["roleName"] = HttpContext.Session.GetString("roleName");
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
