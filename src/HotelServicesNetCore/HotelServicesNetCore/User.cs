using System;
using System.Collections.Generic;

namespace HotelServicesNetCore
{
    public class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Fio { get; set; }
        public string Id { get; set; }
        public Roles.RolesValues Role { get; set; }

        public User(string fio, string login, string password, Roles.RolesValues role)
        {
            Role = role;
            Fio = fio;
            Login = login;
            Password = password;
            Id = Guid.NewGuid().ToString();
        }

        public User(User user)
        {
            Role = user.Role;
            Fio = user.Fio;
            Login = user.Login;
            Password = user.Password;
            Id = Guid.NewGuid().ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            var user = (User) obj;
            return Role == user.Role &&
                   Fio == user.Fio &&
                   Login == user.Login &&
                   Password == user.Password &&
                   Id == user.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Login, Password, Fio, Id, Role);
        }
    }
}