using System;
using ConsoleTest.UI;
using HotelServicesLib;

namespace ConsoleTestNetCore.UI.Commands
{
    public class EnterCommand : ICommand
    {
        private readonly IUsersContainer _usersContainer;
        private readonly Menu[] _mainMenu;

        public string Name { get; }

        public EnterCommand(string name, IUsersContainer usersContainer, Menu[] menu)
        {
            Name = name;
            _usersContainer = usersContainer;
            _mainMenu = menu;
        }

        public void Execute()
        {
            Console.Write("Логин: ");
            string login = Console.ReadLine();

            Console.Write("Пароль: ");
            string password = Console.ReadLine();

            var tmpUser = _usersContainer.GetUserByLogin(login);

            if (tmpUser == null)
            {
                Refresh("Пользователь с таким логином не найден");
                return;
            }

            if (tmpUser.Password == password)
            {
                Console.Clear();
                Console.WriteLine("Авторизация успешна");
                Console.WriteLine();
                Menu.CurrentUser = tmpUser;
                _mainMenu[(int)Menu.CurrentUser.Role].Print();
                _mainMenu[(int)Menu.CurrentUser.Role].SetCommand(_mainMenu[(int)Menu.CurrentUser.Role].ReadCommand());
                _mainMenu[(int)Menu.CurrentUser.Role].Run();
                
            }
            else
            {
                Console.Clear();
                Refresh("Пароль неверный");
            }
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
