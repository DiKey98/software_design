using System;
using ConsoleTest.UI;
using HotelServicesLib;

namespace ConsoleTestNetCore.UI.Commands
{
    public class ViewAllUnpaidServicesCommand : ICommand
    {
        private readonly IOrdersContainer _servicesContainer;
        private readonly Menu _clientMenu;

        public string Name { get; }

        public ViewAllUnpaidServicesCommand(string name, IOrdersContainer servicesContainer, Menu clientMenu)
        {
            _servicesContainer = servicesContainer;
            _clientMenu = clientMenu;
            Name = name;
        }

        public void Execute()
        {
            var services = _servicesContainer.GetOrders(null, false);
            foreach (var service in services)
            {
                Console.WriteLine($"Услуга {service.Service.Name} Стоимость {service.Cost} Клиент {service.Client.Fio}");
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
