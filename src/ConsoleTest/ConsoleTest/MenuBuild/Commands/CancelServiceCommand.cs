using System;
using System.Collections.Generic;
using HotelServicesLib;

namespace ConsoleTest.MenuBuild.Commands
{
    public class CancelServiceCommand: ICommand
    {
        private readonly IServicesContainer _servicesContainer;
        private readonly IUserOperations _userOperations;
        private readonly Menu _clientMenu;

        public string Name { get; }
        
        public CancelServiceCommand(string name, IUserOperations userOperations,  
            IServicesContainer servicesContainer, Menu clientMenu)
        {
            Name = name;
            _servicesContainer = servicesContainer;
            _clientMenu = clientMenu;
            _userOperations = userOperations;
        }

        public void Execute()
        {
            Console.WriteLine("Заказанные услуги:");
            Console.WriteLine();
            PrintServices(_servicesContainer.GetUnPaidServices(Menu.CurrentUser));
            Console.WriteLine();
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
            _userOperations.CancelService(service);
            Console.Clear();
            Console.WriteLine("Услуга успешно отменена");
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

        private void PrintServices(ICollection<IService> services)
        {
            if (services.Count == 0)
            {
                Console.WriteLine("Нет заказанных услуг. Нажмите любую клавишу...");
                Console.ReadKey(false);
                Console.Clear();
                _clientMenu.Print();
                _clientMenu.SetCommand(_clientMenu.ReadCommand());
                _clientMenu.Run();
            }
            foreach (var service in services)
            {
                Console.WriteLine($"Услуга {service.Name} с id = {service.Id}");
            }
        }
    }
}