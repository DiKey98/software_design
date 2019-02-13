using HotelServicesLib;
using System;
using ConsoleTest.Containers;

namespace ConsoleTest.UI.Commands
{
    public class ViewAllServicesCommand : ICommand
    {
        private readonly IOrdersContainer _servicesContainer;
        private readonly Menu _clientMenu;

        public string Name { get; }

        public ViewAllServicesCommand(string name, IOrdersContainer servicesContainer, Menu clientMenu)
        {
            _servicesContainer = servicesContainer;
            _clientMenu = clientMenu;
            Name = name;
        }



        public void Execute()
        {
            var services = _servicesContainer.GetOrders();
            foreach (var service in services)
            {
                Console.WriteLine($"Услуга {service.Service.Name} Стоимость {service.Cost} {service.Service.Measurement}");
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
