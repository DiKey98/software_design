using System;

namespace HotelServicesLib
{
    public class User
    {
        public readonly string Login;
        public readonly string Password;
        public readonly string Fio;
        public readonly string Id;
        public readonly Roles.RolesValues Role;
        
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
    }
}