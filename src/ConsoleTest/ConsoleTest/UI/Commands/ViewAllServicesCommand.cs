using HotelServicesLib;
using System;
using ConsoleTest.Containers;

namespace ConsoleTest.UI.Commands
{
    public class ViewAllServicesCommand : ICommand
    {
        private readonly IServiceInfoContainer _servicesContainer;
        private readonly Menu _clientMenu;

        public string Name { get; }

        public ViewAllServicesCommand(string name, IServiceInfoContainer servicesContainer, Menu clientMenu)
        {
            _servicesContainer = servicesContainer;
            _clientMenu = clientMenu;
            Name = name;
        }



        public void Execute()
        {
            var services = _servicesContainer.GetAvailableServices();
            foreach (var service in services)
            {
                Console.WriteLine($"Услуга {service.Name} Стоимость {service.CostPerUnit} руб/{service.Measurement}");
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
