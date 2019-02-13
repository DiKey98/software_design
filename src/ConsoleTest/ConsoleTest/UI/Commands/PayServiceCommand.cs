using HotelServicesLib;
using System;
using System.Collections.Generic;

namespace ConsoleTest.UI.Commands
{
    public class PayServiceCommand: ICommand
    {
        private readonly IUsersOperations _clientOperations;
        private readonly IOrdersContainer _servicesContainer;
        private readonly IServicesOperations _servicesOperations;
        private readonly Menu _clientMenu;

        public string Name { get; }

        public PayServiceCommand(string name, IUsersOperations clientOperations,
            IOrdersContainer servicesContainer, Menu clientMenu)
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
            PrintServices(_servicesOperations.GetOrders(Menu.CurrentUser, false));
            Console.WriteLine();
            Console.Write("Введите идентификатор услуги: ");
            var idService = Console.ReadLine();
            var service = _servicesContainer.GetOrderById(idService);
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
            _clientOperations.PayService(Menu.CurrentUser, idService);
            Console.Clear();
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

        private void PrintServices(ICollection<Order> services)
        {
            if (services.Count == 0)
            {
                Console.WriteLine("Нет неоплаченных услуг. Нажмите любую клавишу...");
                Console.ReadKey(false);
                Console.Clear();
                _clientMenu.Print();
                _clientMenu.SetCommand(_clientMenu.ReadCommand());
                _clientMenu.Run();
                return;
            }
            foreach (var service in services)
            {
                Console.WriteLine($"Услуга {service.Service.Name} с id = {service.Id}");
            }
        }
    }
}