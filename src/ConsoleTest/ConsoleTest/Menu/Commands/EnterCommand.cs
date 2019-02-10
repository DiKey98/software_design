using HotelServicesLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest.Menu.Commands
{
    class EnterCommand : ICommand
    {
        private IUsersContainer _usersContainer;

        public string Name { get; }

        public EnterCommand(string name)
        {
            Name = name;
        }

        public void Execute()
        {
            Console.Write("Логин: ");
            string login = Console.ReadLine();

            Console.Write("Пароль: ");
            string password = Console.ReadLine();

            var tmpUser = _usersContainer.GetUserByLogin(login);

            if (tmpUser==null)
            {
                Console.WriteLine("Пользователь с таким логином не найден");
            }

            if (tmpUser.Password==password)
            {
                Console.WriteLine("Авторизация успешна");
                MainMenu.CurrentUser = tmpUser;
            }
            else
            {
                Console.WriteLine("Пароль неверный");
            }


            
        }
    }
}
