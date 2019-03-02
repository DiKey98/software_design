using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using HotelServicesNetCore;

namespace ConsoleTestNetCore.UI.Commands
{
    class ViewStatisticPerClientCommand : ICommand
    {
        private readonly IOrdersContainer _ordersContainer;
        private readonly IUsersContainer _userContainer;
        private readonly IRolesContainer _rolesContainer;
        private readonly Menu _clientMenu;


        public string Name { get; }

        public ViewStatisticPerClientCommand(string name, IOrdersContainer ordersContainer, IUsersContainer userContainer, Menu clientMenu)
        {
            _ordersContainer = ordersContainer;
            _clientMenu = clientMenu;
            _userContainer = userContainer;
            Name = name;
        }


        public void Execute()
        {
            Console.WriteLine("Введите период. Формат ДД.ММ.ГГГГ");
            Console.Write("C "); 
            var begin = Console.ReadLine();
            Console.Write("По ");
            var end = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("Выберите пользователя");
            var users = _userContainer.GetUsers();
            foreach (var user in users)
            {
                if (user.Role.Name.ToLower() == "Клиент".ToLower())
                Console.WriteLine($"ФИО {user.Fio} логин {user.Login}");
            }

            Console.WriteLine();
            Console.Write("Введите логин = ");
            var log = Console.ReadLine();
            var us = log == null ? null : _userContainer.GetUserByLogin(log);

            DateTime? beginDate;
            if (begin == "")
            {
                beginDate = null;
            }
            else
            {
                beginDate = DateTime.ParseExact(begin, "dd.MM.yyyy", CultureInfo.CurrentCulture);
            }


            DateTime? endDate;
            if (end == "")
            {
                endDate = null;
            }
            else
            {
                endDate = DateTime.ParseExact(end, "dd.MM.yyyy", CultureInfo.CurrentCulture);
            }

            var services = _ordersContainer.GetOrders(us, true, true, beginDate, endDate);
            foreach (var service in services)
            {
                var timeString = service.OrderDate.ToString("g", CultureInfo.CurrentCulture);
                if (service.IsPaid)
                {
                    Console.WriteLine($"Услуга {service.Service.Name} Стоимость {service.Cost} Дата {timeString} Оплачен");
                } else
                Console.WriteLine($"Услуга {service.Service.Name} Стоимость {service.Cost} Дата {timeString} Не оплачен");
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
