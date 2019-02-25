using System;
using HotelServicesNetCore;

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
                var currentMenu = new Menu();

                switch (Menu.CurrentUser.Role.Name.ToLower())
                {
                    case "администратор":
                        currentMenu = _mainMenu[0];
                        break;

                    case "клиент":
                        currentMenu = _mainMenu[2];
                        break;

                    case "управляющий":
                        currentMenu = _mainMenu[1];
                        break;
                }

                currentMenu.Print();
                currentMenu.SetCommand(currentMenu.ReadCommand());
                currentMenu.Run();
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
