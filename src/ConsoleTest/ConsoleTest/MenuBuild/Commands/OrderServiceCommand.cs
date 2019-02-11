using System;
using System.Collections.Generic;
using ConsoleTest.Services;
using HotelServicesLib;

namespace ConsoleTest.MenuBuild.Commands
{
    public class OrderServiceCommand: ICommand
    {
        private readonly IServicesContainer _servicesContainer;
        private readonly IUserOperations _userOperations;
        private readonly Menu _clientMenu;

        public string Name { get; }

        public OrderServiceCommand(string name, IServicesContainer servicesContainer, 
            IUserOperations userOperations, Menu clientMenu)
        {
            _servicesContainer = servicesContainer;
            _userOperations = userOperations;
            _clientMenu = clientMenu;
            Name = name;
        }

        public void Execute()
        {
            Console.WriteLine("Доступные услуги:");
            Console.WriteLine();
            PrintServices(_servicesContainer.GetAllAvailableServices());
            Console.WriteLine();
            Console.Write("Введите название услуги: ");
            var serviceName = Console.ReadLine();
            if (serviceName == null)
            {
                Refresh("Неверное название услуги");
                return;
            }
            var service = _servicesContainer.GetServiceInfoByName(serviceName);
            if (service == null)
            {
                Refresh("Неверное название услуги");
                return;
            }
            var tmpService = ServicesOptions.ServicesInputs[service.Name.ToLower()];
            _userOperations.OrderService(tmpService());
            Console.Clear();
            Console.WriteLine("Услуга успешно заказана");
            Console.WriteLine();
            Console.WriteLine();
            _clientMenu.Print();
            _clientMenu.SetCommand(_clientMenu.ReadCommand());
            _clientMenu.Run();
        }

        private void Refresh(string message)
        {
            Console.Clear();
            Console.WriteLine(message);
            Console.WriteLine();
            Execute();
        }

        private void PrintServices(IEnumerable<IServiceInfo> services)
        {
            foreach (var service in services)
            {
                Console.WriteLine($"Услуга {service.Name}  {service.Cost} руб.");
            }
        }
    }
}