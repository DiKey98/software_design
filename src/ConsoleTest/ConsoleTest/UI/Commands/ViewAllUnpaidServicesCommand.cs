using System;

namespace ConsoleTest.UI.Commands
{
    public class ViewAllUnpaidServicesCommand : ICommand
    {
        private readonly IServicesContainer _servicesContainer;
        private readonly Menu _clientMenu;

        public string Name { get; }

        public ViewAllUnpaidServicesCommand(string name, IServicesContainer servicesContainer, Menu clientMenu)
        {
            _servicesContainer = servicesContainer;
            _clientMenu = clientMenu;
            Name = name;
        }

        public void Execute()
        {
            var services = _servicesContainer.GetUnPaidServices();
            foreach (var service in services)
            {
                Console.WriteLine($"Услуга {service.Name} Стоимость {service.Cost} Клиент {service.Client.Fio}");
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
