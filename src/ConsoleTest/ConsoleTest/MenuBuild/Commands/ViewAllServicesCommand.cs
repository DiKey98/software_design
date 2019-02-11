using HotelServicesLib;

namespace ConsoleTest.MenuBuild.Commands
{
    public class ViewAllServicesCommand : ICommand
    {
        private readonly IServicesContainer _servicesContainer;

        public string Name { get; }

        public ViewAllServicesCommand(string name, IServicesContainer servicesContainer)
        {
            Name = name;
            _servicesContainer = servicesContainer;
        }

        public void Execute()
        {
            _servicesContainer.GetAllServices();
        }
    }
}
