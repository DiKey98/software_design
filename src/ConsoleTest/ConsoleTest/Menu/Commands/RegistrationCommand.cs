using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelServicesLib;

namespace ConsoleTest.Menu.Commands
{
    class RegistrationCommand : ICommand
    {
        private IUsersContainer _usersContainer;
        private MainMenu _mainMenu;

        public string Name { get; }

        public RegistrationCommand(string name, IUsersContainer usersContainer, MainMenu mainMenu)
        {
            Name = name;
            _usersContainer = usersContainer;
            _mainMenu = mainMenu;
        }
   
        public void Execute()
        {
            Console.Clear();
            Console.Write("Роль (администратор, управляющий, клиент): ");
            string role = Console.ReadLine().ToLower();
            Roles.RolesValues roleId;
            if (Roles.Values.ContainsKey(role))
            {
                roleId = Roles.Values[role];

            }
            else
            {
                Refresh("Неверно указна роль");
                return;
            }

            Console.Write("ФИО: ");
            string fio = Console.ReadLine();

            Console.Write("Логин: ");
            string login = Console.ReadLine();

            Console.Write("Пароль: ");
            string password = Console.ReadLine();

            User user = new User(fio, login, password, roleId);
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
            _mainMenu.Print();
            var command = _mainMenu.ReadCommand();
            _mainMenu.SetCommand(command);
            _mainMenu.Run();
        }
    }
}

