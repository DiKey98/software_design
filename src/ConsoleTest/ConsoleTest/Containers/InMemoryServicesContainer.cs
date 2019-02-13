using System.Collections.Generic;
using System.Linq;
using HotelServicesLib;

namespace ConsoleTest.Containers
{
    public class InMemoryServicesContainer: IServiceInfoContainer
    {
        private static InMemoryServicesContainer _servicesContainer;

        private readonly List<ServiceInfo> _container;

        private InMemoryServicesContainer()
        {
            _container = new List<ServiceInfo>();
        }

        public static InMemoryServicesContainer GetInstance()
        {
            return _servicesContainer ?? (_servicesContainer = new InMemoryServicesContainer());
        }

        public void AddServiceInfo(ServiceInfo service)
        {
            if (service != null)
            {
                _container.Add(service);
            }
        }

        public void RemoveServiceInfo(ServiceInfo service)
        {
            _container.Remove(service);
        }

        public ServiceInfo GetServiceInfoById(string id)
        {
            return _container.FirstOrDefault(service => service.Id.Equals(id));
        }

        public ServiceInfo GetServiceInfoByName(string name)
        {
            return _container.FirstOrDefault(service => service.Name.Equals(name));
        }
    }
}