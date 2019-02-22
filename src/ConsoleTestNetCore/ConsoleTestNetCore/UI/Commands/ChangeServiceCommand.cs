using System;
using System.Collections.Generic;
using ConsoleTest.UI;
using HotelServicesLib;

namespace ConsoleTestNetCore.UI.Commands
{
    public class ChangeServiceCommand: ICommand
    {
        private readonly IServicesOperations _serviceOperations;
        private readonly IServiceInfoContainer _serviceInfoContainer;
        private readonly Menu _managerMenu;

        public ChangeServiceCommand(string name, IServicesOperations serviceOperations, IServiceInfoContainer serviceInfoContainer, 
            Menu managerMenu)
        {
            Name = name;
            _serviceOperations = serviceOperations;
            _managerMenu = managerMenu;
            _serviceInfoContainer = serviceInfoContainer;
        }

        public string Name { get; }

        public void Execute()
        {
            Console.WriteLine("Список услуг:");
            Console.WriteLine();
            PrintServices(_serviceInfoContainer.GetAvailableServices());
            Console.WriteLine();
            Console.WriteLine("Введите id услуги для изменения: ");
            var serviceId = Console.ReadLine();
            var oldService = _serviceInfoContainer.GetServiceInfoById(serviceId);
            if (oldService == null)
            {
                Refresh("несуществующий id услуги");
                return;
            }
            Console.Clear();
            Console.WriteLine("Название услуги:");
            var name = Console.ReadLine();
            Console.WriteLine("Стоимость ед. услуги:");
            var cost = decimal.Parse(Console.ReadLine());
            Console.WriteLine("Ед. измерения:");
            var measurement = Console.ReadLine();
            var newService = new ServiceInfo(oldService.Id, name, cost, measurement);
            _serviceOperations.ChangeServiceInfo(oldService, newService);
        }

        private void PrintServices(ICollection<ServiceInfo> services)
        {
            if (services.Count == 0)
            {
                Console.WriteLine("Нет услуг. Нажмите любую клавишу...");
                Console.ReadKey(false);
                Console.Clear();
                _managerMenu.Print();
                _managerMenu.SetCommand(_managerMenu.ReadCommand());
                _managerMenu.Run();
                return;
            }
            foreach (var service in services)
            {
                Console.WriteLine($"Услуга {service.Name} id = {service.Id}");
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