using System;

namespace ConsoleTest.UI.Commands
{
    public class ChangeServiceCommand: ICommand
    {
        private IServiceOperations _serviceOperations;

        public ChangeServiceCommand(string name, IServiceOperations serviceOperations)
        {
            Name = name;
            _serviceOperations = serviceOperations;
        }

        public string Name { get; }

        public void Execute()
        {
            Console.WriteLine("Введите название услуги для изменения: ");
            var serviceName = Console.ReadLine();

        }
    }
}