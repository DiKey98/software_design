﻿using System.Collections.Generic;

namespace HotelServicesNetCore
{
    public interface IUsersContainer
    {
        void AddUser(User user);
        void RemoveUser(User user);
        User GetUserById(string id);
        User GetUserByLogin(string login);
        ICollection<User> GetUsers();
    }
}