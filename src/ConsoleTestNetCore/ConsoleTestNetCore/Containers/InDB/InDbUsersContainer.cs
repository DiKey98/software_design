using System.Collections.Generic;
using System.Linq;
using HotelServicesNetCore;

namespace ConsoleTestNetCore.Containers.InDB
{
    public class InDbUsersContainer : IUsersContainer
    {
        private readonly HotelServicesDbContext _dbContext;

        public InDbUsersContainer(HotelServicesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddUser(User user)
        {
            if (user == null)
            {
                return;
            }
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public void RemoveUser(User user)
        {
            if (user == null)
            {
                return;
            }
            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();
        }

        public User GetUserById(string id)
        {
            return
                (from user in _dbContext.Users
                join role in _dbContext.Roles on user.RoleId equals role.Id
                where user.Id == id
                select new User
                {
                    Fio = user.Fio,
                    Login = user.Login,
                    Password = user.Password,
                    Id = user.Id,
                    Role = role
                })
                .FirstOrDefault();
        }

        public User GetUserByLogin(string login)
        {
            return
                (from user in _dbContext.Users
                join role in _dbContext.Roles on user.RoleId equals role.Id
                where user.Login == login
                select new User
                {
                    Fio = user.Fio,
                    Login = user.Login,
                    Password = user.Password,
                    Id = user.Id,
                    Role = role
                })
                .FirstOrDefault();
        }

        public ICollection<User> GetUsers()
        {
            return
                (from user in _dbContext.Users
                join role in _dbContext.Roles on user.RoleId equals role.Id
                select new User
                {
                    Fio = user.Fio,
                    Login = user.Login,
                    Password = user.Password,
                    Id = user.Id,
                    Role = role
                })
                .ToList();
        }
    }
}