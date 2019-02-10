using HotelServicesLib;

namespace ConsoleTest
{
    public class InMemoryClientOperations: IClientOperations
    {
        private static InMemoryClientOperations _operations;

        private readonly IClientsContainer _clientsContainer;
        private readonly IServicesContainer _servicesContainer;

        private InMemoryClientOperations(IServicesContainer servicesContainer, IClientsContainer clientsContainer)
        {
            _servicesContainer = servicesContainer;
            _clientsContainer = clientsContainer;
        }

        public static InMemoryClientOperations GetInstance(IServicesContainer servicesContainer, IClientsContainer clientsContainer)
        {
            return _operations ?? (_operations = new InMemoryClientOperations(servicesContainer, clientsContainer));
        }

        public void ChangeClient(Client oldClient, Client newClient)
        {
            _clientsContainer.RemoveClient(oldClient);
            _clientsContainer.AddClient(newClient);
        }

        public void OrderService(IService service)
        {
            if (service.IsPaid)
            {
                return;
            }
            service.IsPaid = false;
            _servicesContainer.AddService(service);
        }

        public void PayService(IService service)
        {
            if (service.IsPaid)
            {
                return;
            }
            _servicesContainer.RemoveService(service);
            service.IsPaid = true;
            _servicesContainer.AddService(service);
        }

        public void CancelService(IService service)
        {
            if (service.IsPaid)
            {
                return;
            }
            _servicesContainer.RemoveService(service);
        }
    }
}