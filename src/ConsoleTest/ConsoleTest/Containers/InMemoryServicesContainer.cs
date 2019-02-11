using System.Collections.Generic;
using System.Linq;
using HotelServicesLib;

namespace ConsoleTest.Containers
{
    public class InMemoryServicesContainer: IServicesContainer
    {
        private static InMemoryServicesContainer _servicesContainer;

        private readonly List<IService> _container;
        private readonly List<IServiceInfo> _availableServices;

        public InMemoryServicesContainer(IEnumerable<IServiceInfo> availableServices)
        {
            _availableServices = availableServices as List<IServiceInfo>;
            _container = new List<IService>();
        }

        public static InMemoryServicesContainer GetInstance(IEnumerable<IServiceInfo> availableServices)
        {
            return _servicesContainer ?? (_servicesContainer = new InMemoryServicesContainer(availableServices));
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

        public IServiceInfo GetServiceInfoByName(string name)
        {
            return _availableServices.FirstOrDefault(service => service.Name == name);
        }

        public ICollection<IServiceInfo> GetAllAvailableServices()
        {
            return _availableServices.GetRange(0, _availableServices.Count);
        }

        public ICollection<IService> GetAllServices(User user = null)
        {
            return user == null
                ? _container.GetRange(0, _container.Count)
                : _container.Where(service => service.Client.Equals(user)).ToList();
            
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