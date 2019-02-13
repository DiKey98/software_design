using HotelServicesLib;

namespace ConsoleTest.Operations
{
    public class InMemoryServiceOperations: IServiceOperations
    {
        private static InMemoryServiceOperations _serviceOperations;

        private readonly IServicesContainer _servicesContainer;

        private InMemoryServiceOperations(IServicesContainer servicesContainer)
        {
            _servicesContainer = servicesContainer;
        }

        public static InMemoryServiceOperations GetInstance(IServicesContainer servicesContainer)
        {
            if (_serviceOperations == null)
            {
               _serviceOperations = new InMemoryServiceOperations(servicesContainer);
            }
            return _serviceOperations;
        }

        public void ChangeService(IService oldService, IService newService)
        {
            _servicesContainer.RemoveService(oldService);
            _servicesContainer.AddService(newService);
        }
    }
}