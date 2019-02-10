using System.Collections.Generic;
using System.Linq;
using HotelServicesLib;

namespace ConsoleTest.Containers
{
    public class InMemoryServicesContainer: IServicesContainer
    {
        private static InMemoryServicesContainer _operations;

        private readonly List<IService> _container;

        public InMemoryServicesContainer()
        {
            _container = new List<IService>();
        }

        public static InMemoryServicesContainer GetInstance()
        {
            return _operations ?? (_operations = new InMemoryServicesContainer());
        }

        public void AddService(IService service)
        {
            _container.Add(service);
        }

        public void RemoveService(IService service)
        {
            _container.Remove(service);
        }

        public IService GetServiceById(string id)
        {
            return _container.FirstOrDefault(service => service.Id == id);
        }

        public ICollection<IService> GetAllServices()
        {
            return _container.GetRange(0, _container.Count);
        }

        public ICollection<IService> GetPaidServices(User user = null)
        {
            return user == null 
                ? _container.Where(service => service.IsPaid).ToList() 
                : _container.Where(service => service.IsPaid && service.Client.Equals(user)).ToList();
        }

        public ICollection<IService> GetUnPaidServices(User client = null)
        {
            return client == null
                ? _container.Where(service => !service.IsPaid).ToList()
                : _container.Where(service => !service.IsPaid && service.Client.Equals(client)).ToList();
        }
    }
}