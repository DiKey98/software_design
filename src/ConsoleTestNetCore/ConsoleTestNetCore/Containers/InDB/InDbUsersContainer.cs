using System.Collections.Generic;
using System.Linq;
using HotelServicesNetCore;
using Microsoft.EntityFrameworkCore;

namespace ConsoleTestNetCore.Containers.InDB
{
    public class InDbUsersContainer : IUsersContainer
    {
        public void AddUser(User user)
        {
            if (user == null)
            {
                return;
            }
            using (var db = new HotelServicesDbContext())
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
        }

        public void RemoveUser(User user)
        {
            if (user == null)
            {
                return;
            }
            using (var db = new HotelServicesDbContext())
            {
                db.Users.Remove(user);
                db.SaveChanges();
            }
        }

        public User GetUserById(string id)
        {
            using (var db = new HotelServicesDbContext())
            {
                return
                    (from user in db.Users
                        join role in db.Roles on user.RoleId equals role.Id
                        where user.Id == id
                        select new User
                        {
                            Fio = user.Fio,
                            Login = user.Login,
                            Password = user.Password,
                            Id = user.Id,
                            Role = role
                        })
                    .AsNoTracking().FirstOrDefault();
            }
        }

        public User GetUserByLogin(string login)
        {
            using (var db = new HotelServicesDbContext())
            {
                return
                    (from user in db.Users
                        join role in db.Roles on user.RoleId equals role.Id
                        where user.Login == login
                        select new User
                        {
                            Fio = user.Fio,
                            Login = user.Login,
                            Password = user.Password,
                            Id = user.Id,
                            Role = role
                        })
                    .AsNoTracking().FirstOrDefault();
            } 
        }

        public ICollection<User> GetUsers()
        {
            using (var db = new HotelServicesDbContext())
            {
                return
                    (from user in db.Users
                        join role in db.Roles on user.RoleId equals role.Id
                        select new User
                        {
                            Fio = user.Fio,
                            Login = user.Login,
                            Password = user.Password,
                            Id = user.Id,
                            Role = role
                        })
                    .AsNoTracking().ToList();
            }  
        }
    }
}