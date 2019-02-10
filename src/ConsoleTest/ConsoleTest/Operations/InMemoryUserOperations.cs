using HotelServicesLib;

namespace ConsoleTest.Operations
{
    public class InMemoryUserOperations: IUserOperations
    {
        private static InMemoryUserOperations _operations;

        private readonly IUsersContainer _usersContainer;
        private readonly IServicesContainer _servicesContainer;

        private InMemoryUserOperations(IUsersContainer usersContainer, IServicesContainer servicesContainer)
        {
            _usersContainer = usersContainer;
            _servicesContainer = servicesContainer;
        }

        public static InMemoryUserOperations GetInstance(IUsersContainer container, IServicesContainer servicesContainer)
        {
            return _operations ?? (_operations = new InMemoryUserOperations(container, servicesContainer));
        }

        public void ChangeUser(User oldUser, User newUser)
        {
            _usersContainer.RemoveUser(oldUser);
            _usersContainer.AddUser(newUser);
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