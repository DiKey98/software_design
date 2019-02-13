using System;
using System.Globalization;

namespace ConsoleTest.UI.Commands
{
    public class ViewAllPaidServicesCommand : ICommand
    {
        private readonly IServicesContainer _servicesContainer;
        private readonly Menu _clientMenu;

        public string Name { get; }

        public ViewAllPaidServicesCommand(string name, IServicesContainer servicesContainer, Menu clientMenu)
        {
            _servicesContainer = servicesContainer;
            _clientMenu = clientMenu;
            Name = name;
        }


        public void Execute()
        {
            var services = _servicesContainer.GetPaidServices();
            foreach (var service in services)
            {
                var timeString = service.TimeOrder.ToString("g", CultureInfo.CurrentCulture);
                Console.WriteLine($"Услуга {service.Name} Стоимость {service.Cost} Дата {timeString}");
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
