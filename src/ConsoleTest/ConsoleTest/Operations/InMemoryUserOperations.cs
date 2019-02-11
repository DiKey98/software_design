using HotelServicesLib;

namespace ConsoleTest.Operations
{
    public class InMemoryUserOperations: IUserOperations
    {
        private static InMemoryUserOperations _operations;

        private readonly IUsersContainer _usersContainer;
        private readonly IServicesContainer _servicesContainer;

        private User _currentUser;

        private InMemoryUserOperations(IUsersContainer usersContainer, IServicesContainer servicesContainer)
        {
            _usersContainer = usersContainer;
            _servicesContainer = servicesContainer;
        }

        public static InMemoryUserOperations GetInstance(IUsersContainer container, IServicesContainer servicesContainer)
        {
            if (_operations == null)
            {
                _operations = new InMemoryUserOperations(container, servicesContainer);
            }
            return _operations;
        }

        public void SetCurrentUser(User user)
        {
            _currentUser = user;
        }

        public void ChangeUser(User newUser)
        {
            _usersContainer.RemoveUser(_currentUser);
            _usersContainer.AddUser(newUser);
            _currentUser = newUser;
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