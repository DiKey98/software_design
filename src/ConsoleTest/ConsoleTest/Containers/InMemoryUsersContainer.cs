using System.Collections.Generic;
using System.Linq;
using HotelServicesLib;

namespace ConsoleTest.Containers
{
    public class InMemoryUsersContainer : IUsersContainer
    {
        //private static InMemoryUsersContainer _operations;

        private readonly List<User> _container;

        public InMemoryUsersContainer()
        {
            _container = new List<User>();
        }

        public InMemoryUsersContainer(IEnumerable<User> users)
        {
            _container = users as List<User>;
        }

        //public static InMemoryUsersContainer GetInstance()
        //{
        //    return _operations ?? (_operations = new InMemoryUsersContainer());
        //}

        //public static InMemoryUsersContainer GetInstance(IEnumerable<User> users)
        //{
        //    return _operations ?? (_operations = new InMemoryUsersContainer(users));
        //}

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

        public ICollection<User> GetUsers()
        {
            return _container.Where(user => true).ToList();
        }
    }
}