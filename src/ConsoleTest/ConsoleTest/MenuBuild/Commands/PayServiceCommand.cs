using System;
using System.Collections.Generic;
using HotelServicesLib;

namespace ConsoleTest.MenuBuild.Commands
{
    public class PayServiceCommand: ICommand
    {
        private readonly IUserOperations _clientOperations;
        private readonly IServicesContainer _servicesContainer;
        private readonly Menu _clientMenu;

        public string Name { get; }

        public PayServiceCommand(string name, IUserOperations clientOperations, 
            IServicesContainer servicesContainer, Menu clientMenu)
        {
            Name = name;
            _clientOperations = clientOperations;
            _clientMenu = clientMenu;
            _servicesContainer = servicesContainer;
        }

        public void Execute()
        {
            Console.WriteLine("Неоплаченные услуги:");
            Console.WriteLine();
            PrintServices(_servicesContainer.GetUnPaidServices(Menu.CurrentUser));
            Console.Write("Введите идентификатор услуги: ");
            var idService = Console.ReadLine();
            var service = _servicesContainer.GetServiceById(idService);
            if (service == null)
            {
                Refresh("Неверный id услуги");
                return;
            }
            if (service.IsPaid)
            {
                Refresh("Услуга уже оплачена");
                return;
            }
            _clientOperations.PayService(service);
            Console.WriteLine("Услуга успешно оплачена");
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

        private void PrintServices(IEnumerable<IService> services)
        {
            foreach (var service in services)
            {
                Console.WriteLine($"Услуга {service.Name} с id = {service.Id}");
            }
        }
    }
}