using System;

namespace HotelServicesLib
{
    public class User
    {
        private readonly string _login;
        private readonly string _password;
        private readonly string _fio;

        public readonly string Id;
        public readonly Roles Role;
        
        public User(string fio, string login, string password, Roles role)
        {
            Role = role;
            _fio = fio;
            _login = login;
            _password = password;
            Id = Guid.NewGuid().ToString();
        }

        public User(User user)
        {
            Role = user.Role;
            _fio = user._fio;
            _login = user._login;
            _password = user._password;
            Id = Guid.NewGuid().ToString();
        }
    }
}