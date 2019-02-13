using System;
using System.Globalization;
using HotelServicesLib;

namespace ConsoleTest.UI.Commands
{
    public class ViewAllPaidServicesCommand : ICommand
    {
        private readonly IOrdersContainer _ordersContainer;
        private readonly Menu _clientMenu;


        public string Name { get; }

        public ViewAllPaidServicesCommand(string name, IOrdersContainer ordersContainer, Menu clientMenu)
        {
            _ordersContainer = ordersContainer;
            _clientMenu = clientMenu;
            Name = name;
        }


        public void Execute()
        {
            var services = _ordersContainer.GetOrders();
            foreach (var service in services)
            {
                var timeString = service.OrderDate.ToString("g", CultureInfo.CurrentCulture);
                Console.WriteLine($"Услуга {service.Service.Name} Стоимость {service.Cost} Дата {timeString}");
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
