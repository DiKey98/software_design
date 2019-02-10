using System.Collections.Generic;
using System.Linq;
using HotelServicesLib;

namespace ConsoleTest
{
    public class InMemoryClientsContainer: IUsersContainer
    {
        private static InMemoryClientsContainer _operations;

        private readonly List<User> _container;

        public InMemoryClientsContainer()
        {
            _container = new List<User>();
        }

        public static InMemoryClientsContainer GetInstance()
        {
            return _operations ?? (_operations = new InMemoryClientsContainer());
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
            return _container.FirstOrDefault(client => client.Id == id);
        }

        public User GetUserByLogin(string login)
        {
            return _container.FirstOrDefault(client => client.Login == login);
        }
    }
}