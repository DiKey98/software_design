using HotelServicesLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest.Menu.Commands
{
    public class ViewAllPaidServicesCommand : ICommand
    {
        private readonly IServicesContainer _servicesContainer;

        public string Name { get; }

        public ViewAllPaidServicesCommand(string name, IServicesContainer servicesContainer)
        {
            Name = name;
            _servicesContainer = servicesContainer;
        }

        public void Execute()
        {
            _servicesContainer.GetPaidServices();
        }
    }
}
