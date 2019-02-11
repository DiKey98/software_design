using HotelServicesLib;

namespace ConsoleTest.MenuBuild.Commands
{
    public class ViewAllUnpaidServicesCommand : ICommand
    {
        private readonly IServicesContainer _servicesContainer;

        public string Name { get; }

        public ViewAllUnpaidServicesCommand(string name, IServicesContainer servicesContainer)
        {
            Name = name;
            _servicesContainer = servicesContainer;
        }

        public void Execute()
        {
            _servicesContainer.GetUnPaidServices();
        }
    }
}
