using System;
using System.Collections.Generic;
using ConsoleTest.UI;
using HotelServicesNetCore;

namespace ConsoleTestNetCore.UI.Commands
{
    public class OrderServiceCommand: ICommand
    {
        private readonly IServiceInfoContainer _serviceInfoContainer;
        private readonly IUsersOperations _userOperations;
        private readonly Menu _clientMenu;

        public string Name { get; }

        public OrderServiceCommand(string name, IServiceInfoContainer serviceInfoContainer, 
            IUsersOperations userOperations, Menu clientMenu)
        {
            _serviceInfoContainer = serviceInfoContainer;
            _userOperations = userOperations;
            _clientMenu = clientMenu;
            Name = name;
        }

        public void Execute()
        {
            Console.WriteLine("Доступные услуги:");
            Console.WriteLine();
            PrintServices(_serviceInfoContainer.GetAvailableServices());
            Console.WriteLine();
            Console.Write("Введите название услуги: ");
            var serviceName = Console.ReadLine();
            if (serviceName == null)
            {
                Refresh("Неверное название услуги");
                return;
            }
            var service = _serviceInfoContainer.GetServiceInfoByName(serviceName);
            if (service == null)
            {
                Refresh("Неверное название услуги");
                return;
            }
            Console.Write("Объём услуги (ед.): ");
            var units = uint.Parse(Console.ReadLine());
            _userOperations.OrderService(Menu.CurrentUser, service.Name, units);
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

        private void PrintServices(IEnumerable<ServiceInfo> services)
        {
            foreach (var service in services)
            {
                Console.WriteLine($"Услуга {service.Name} {service.CostPerUnit}руб./{service.Measurement} id = {service.Id}");
            }
        }
    }
}