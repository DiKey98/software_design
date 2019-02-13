using System;
using System.Collections.Generic;

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
            var hashCode = -1601222816;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Login);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Password);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Fio);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Id);
            hashCode = hashCode * -1521134295 + Role.GetHashCode();
            return hashCode;
        }
    }
}