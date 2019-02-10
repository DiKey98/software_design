using HotelServicesLib;

namespace ConsoleTest
{
    public class InMemoryUserOperations: IUserOperations
    {
        private static InMemoryUserOperations _operations;
        private readonly IUsersContainer _container;

        private InMemoryUserOperations(IUsersContainer container)
        {
            _container = container;
        }

        public static InMemoryUserOperations GetInstance(IUsersContainer container)
        {
            return _operations ?? (_operations = new InMemoryUserOperations(container));
        }

        public void ChangeUser(User oldUser, User newUser)
        {
            _container.RemoveUser(oldUser);
            _container.AddUser(newUser);
        }
    }
}