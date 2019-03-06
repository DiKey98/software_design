using System;
using System.Linq;
using Castle.Core.Internal;
using HotelServicesNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WebServer.Models;

namespace WebServer.Helpers
{
    public static class AuthorizationHelper
    {
        public static void AuthorizeInDb(string sessionId, string userId)
        {
            ClearOldEntities(userId);

            using (var db = new HotelServicesDbContext())
            {
                var a = new Authorization
                {
                    Id = Guid.NewGuid().ToString(),
                    SessionId = sessionId,
                    User = db.Users.FirstOrDefault(u => u.Id == userId)
                };
                db.Authorizations.Add(a);
                db.SaveChanges();
            }
        }

        public static bool IsAuthorizedInDb(string sessionId)
        {
            using (var db = new HotelServicesDbContext())
            {
                var a = db.Authorizations.AsNoTracking().FirstOrDefault(auth => auth.SessionId == sessionId);
                if (a == null)
                {
                    return false;
                } 
            }
            return true;
        }

        public static void ClearOldEntities(string userId = null, string sessionId = null)
        {
            if (userId == null && sessionId == null)
            {
                return;
            }

            if (userId != null)
            {
                using (var db = new HotelServicesDbContext())
                {
                    var a = (from auth in db.Authorizations
                        join u in db.Users on auth.User.Id equals u.Id
                        where auth.User.Id == userId
                        select new Authorization
                        {
                            Id = auth.Id,
                            User = u,
                            SessionId = auth.SessionId
                        }).ToList();

                    if (a.Count <= 0)
                    {
                        return;
                    }

                    db.Authorizations.RemoveRange(a);
                    db.SaveChanges();
                }
                return;
            }

            using (var db = new HotelServicesDbContext())
            {
                var a = (from auth in db.Authorizations
                    join u in db.Users on auth.User.Id equals u.Id
                    where auth.SessionId == sessionId
                    select new Authorization
                    {
                        Id = auth.Id,
                        User = u,
                        SessionId = auth.SessionId
                    }).ToList();

                if (a.Count <= 0)
                {
                    return;
                }

                db.Authorizations.RemoveRange(a);
                db.SaveChanges();
            }
        }

        public static void AddUserToDb(string fio, string login, string password, string roleName)
        {
            if (fio.IsNullOrEmpty() ||
                login.IsNullOrEmpty() ||
                password.IsNullOrEmpty() ||
                roleName.IsNullOrEmpty())
            {
                return;
            }

            using (var db = new HotelServicesDbContext())
            {
                var role = db.Roles.FirstOrDefault(
                    r => string.Equals(r.Name, roleName, StringComparison.CurrentCultureIgnoreCase));

                if (role == null)
                {
                    return;
                }

                var user = new User
                {
                    Id = Guid.NewGuid().ToString(),
                    Fio = fio,
                    Login = login,
                    Password = password,
                    Role = role
                };

                db.Users.Add(user);
                db.SaveChanges();
            }   
        }

        public static bool IsExistsLogin(string login)
        {
            using (var db = new HotelServicesDbContext())
            {
                var user = db.Users.AsNoTracking().FirstOrDefault(u => u.Login == login);
                if (user != null)
                {
                    return true;
                }
            }
            return false;
        }
    }
}