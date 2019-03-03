using System;
using System.Globalization;
using System.Linq;
using HotelServicesNetCore;

namespace ConsoleTestNetCore.UI.Commands
{
    public class ViewAttendanceStatiscticsCommand : ICommand
    {
        private readonly IOrdersContainer _ordersContainer;
        private readonly Menu _clientMenu;

        public ViewAttendanceStatiscticsCommand(string name, IOrdersContainer ordersContainer, Menu clientMenu)
        {
            Name = name;
            _ordersContainer = ordersContainer;
            _clientMenu = clientMenu;
        }

        public string Name { get; }

        public void Execute()
        {
            Console.Write("Начальная дата (Enter для пропуска): ");
            var startDateStr = Console.ReadLine();
            DateTime? startDate;
            if (string.IsNullOrEmpty(startDateStr))
            {
                startDate = null;
            }
            else
            {
                startDate = DateTime.ParseExact(startDateStr, "dd.MM.yyyy", CultureInfo.CurrentCulture);
            }

            Console.Write("Конечная дата (Enter для пропуска): ");
            var endDateStr = Console.ReadLine();
            DateTime? endDate;
            if (string.IsNullOrEmpty(endDateStr))
            {
                endDate = null;
            }
            else
            {
                endDate = DateTime.ParseExact(startDateStr, "dd.MM.yyyy", CultureInfo.CurrentCulture);
            }

            var ordes = _ordersContainer.GetOrders(null, from: startDate, to: endDate);

            if (ordes.Count == 0)
            {
                Console.WriteLine();
                Console.WriteLine("Нет заказов за указанный период");

                Console.WriteLine();
                Console.WriteLine("Для продолжения нажмите любую клавишу");
                Console.ReadKey(false);
                Console.Clear();
                _clientMenu.Print();
                _clientMenu.SetCommand(_clientMenu.ReadCommand());
                _clientMenu.Run();
                return;
            }

            foreach (var line in ordes.GroupBy(o => o.User)
                .Select(group => new {
                    User = group.Key,
                    Count = group.Count()
                })
                .OrderBy(x => x.User.Login))
            {
                Console.WriteLine($"{line.User.Fio} {line.User.Login} сделал {line.Count} заказов за указанный период");
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