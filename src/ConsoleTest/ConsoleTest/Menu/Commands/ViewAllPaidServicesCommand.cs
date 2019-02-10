using HotelServicesLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest.Menu.Commands
{
    class ViewAllPaidServicesCommand : ICommand
    {
        private IServicesContainer _servicesContainer;

        public string Name { get; }

        public ViewAllPaidServicesCommand(string name)
        {
           Name = name;
        }

        public void Execute()
        {
            _servicesContainer.GetPaidServices();
        }
    }
}
