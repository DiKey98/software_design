using HotelServicesLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest.Menu.Commands
{
    class ViewAllUnpaidServicesCommand : ICommand
    {
        private IServicesContainer _servicesContainer;

        public string Name { get; }

        public ViewAllUnpaidServicesCommand(string name)
        {
            Name = name;
        }

        public void Execute()
        {
            _servicesContainer.GetUnPaidServices();
        }
    }
}
