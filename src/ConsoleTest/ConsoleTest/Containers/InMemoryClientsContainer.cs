using System.Collections.Generic;
using System.Linq;
using HotelServicesLib;

namespace ConsoleTest
{
    public class InMemoryClientsContainer: IClientsContainer
    {
        private static InMemoryClientsContainer _operations;

        private readonly List<Client> _container;

        public InMemoryClientsContainer()
        {
            _container = new List<Client>();
        }

        public static InMemoryClientsContainer GetInstance()
        {
            return _operations ?? (_operations = new InMemoryClientsContainer());
        }
        public void AddClient(Client client)
        {
            _container.Add(client);
        }

        public void RemoveClient(Client client)
        {
            _container.Remove(client);
        }

        public Client GetClientById(string id)
        {
            return _container.FirstOrDefault(client => client.Id == id);
        }
    }
}