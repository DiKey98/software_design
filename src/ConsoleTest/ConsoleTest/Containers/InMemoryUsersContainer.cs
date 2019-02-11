using HotelServicesLib;

namespace ConsoleTest.Containers
{
    using System.Collections.Generic;
    using System.Linq;
    using HotelServicesLib;

    namespace ConsoleTest
    {
        public class InMemoryUsersContainer : IUsersContainer
        {
            private static InMemoryUsersContainer _operations;

            private readonly List<User> _container;

            private InMemoryUsersContainer()
            {
                _container = new List<User>();
            }

            private InMemoryUsersContainer(IEnumerable<User> users)
            {
                _container = users as List<User>;
            }

            public static InMemoryUsersContainer GetInstance()
            {
                if (_operations == null)
                {
                    _operations = new InMemoryUsersContainer();
                }
                return _operations;
            }

            public static InMemoryUsersContainer GetInstance(IEnumerable<User> users)
            {
                if (_operations == null)
                {
                    _operations = new InMemoryUsersContainer(users);
                }
                return _operations;
            }

            public void AddUser(User client)
            {
                _container.Add(client);
            }

            public void RemoveUser(User client)
            {
                _container.Remove(client);
            }

            public User GetUserById(string id)
            {
                return _container.FirstOrDefault(client => string.Equals(client.Id, id));
            }

            public User GetUserByLogin(string login)
            {
                return _container.FirstOrDefault(client => string.Equals(client.Login, login));
            }
        }
    }
}