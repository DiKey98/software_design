﻿using System;
using HotelServicesLib;

namespace ConsoleTest.MenuBuild.Commands
{
    public class RegistrationCommand : ICommand
    {
        private readonly IUsersContainer _usersContainer;
        private readonly Menu _mainMenu;

        public string Name { get; }

        public RegistrationCommand(string name, IUsersContainer usersContainer, Menu mainMenu)
        {
            Name = name;
            _usersContainer = usersContainer;
            _mainMenu = mainMenu;
        }
   
        public void Execute()
        {
            Console.Write("Роль (администратор, управляющий, клиент): ");
            var role = Console.ReadLine()?.ToLower();
            Roles.RolesValues roleId;
            if (role != null && Roles.Values.ContainsKey(role))
            {
                roleId = Roles.Values[role];

            }
            else
            {
                Refresh("Неверно указана роль");
                return;
            }

            Console.Write("ФИО: ");
            var fio = Console.ReadLine();

            Console.Write("Логин: ");
            var login = Console.ReadLine();

            Console.Write("Пароль: ");
            var password = Console.ReadLine();

            var user = new User(fio, login, password, roleId);
            var tmpUser = _usersContainer.GetUserByLogin(user.Login);
            if (tmpUser != null)
            {
                Refresh("Логин уже существует");
                return;
            }
            _usersContainer.AddUser(user);
            Refresh("Регистрация прошла успешно");
        }

        private void Refresh(string message)
        {
            Console.Clear();
            Console.WriteLine(message);
            Console.WriteLine();
            Execute();
        }
    }
}

