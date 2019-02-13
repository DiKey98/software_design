﻿using HotelServicesLib;
using System;

namespace ConsoleTest.UI.Commands
{
    public class ViewAllUsers : ICommand
    {
        private readonly IUsersContainer _userContainer;
        private readonly Menu _clientMenu;

        public string Name { get; }


        public ViewAllUsers(string name, IUsersContainer userContainer, Menu clientMenu)
        {
            _userContainer = userContainer;
            _clientMenu = clientMenu;
            Name = name;
        }


        public void Execute()
        {
            var users = _userContainer.GetUsers();
            foreach (var user in users)
            {
                Console.WriteLine($"ФИО {user.Fio} логин {user.Login}");
            }
            Console.WriteLine("Для продолжения нажмите любую клавишу");
            Console.ReadKey(false);
            Console.Clear();
            _clientMenu.Print();
            _clientMenu.SetCommand(_clientMenu.ReadCommand());
            _clientMenu.Run();
        }
    }
}
