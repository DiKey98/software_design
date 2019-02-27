using System;
using System.Collections.Generic;
using System.Linq;
using HotelServicesNetCore;

namespace ConsoleTestNetCore.Containers.InMemory
{
    public class InMemoryServicesContainer: IServiceInfoContainer
    {
        //private static InMemoryServicesContainer _servicesContainer;

        private readonly List<ServiceInfo> _container;

        public InMemoryServicesContainer()
        {
            _container = new List<ServiceInfo>();
        }

        public InMemoryServicesContainer(IEnumerable<ServiceInfo> services)
        {
            _container = services as List<ServiceInfo>;
        }

        //public static InMemoryServicesContainer GetInstance()
        //{
        //    return _servicesContainer ?? (_servicesContainer = new InMemoryServicesContainer());
        //}

        //public static InMemoryServicesContainer GetInstance(IEnumerable<ServiceInfo> services)
        //{
        //    return _servicesContainer ?? (_servicesContainer = new InMemoryServicesContainer(services));
        //}

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

        public void UpdateService(ServiceInfo oldService, ServiceInfo newService)
        {
            var contains = false;
            foreach (var t in _container)
            {
                if (t.Id != oldService.Id)
                {
                    continue;
                }
                t.IsDeprecated = true;
                contains = true;
            }

            if (!contains)
            {
                return;
            }

            _container.Add(newService);
        }

        public ServiceInfo GetServiceInfoById(string id)
        {
            return _container.FirstOrDefault(service => service.Id.Equals(id));
        }

        public ServiceInfo GetServiceInfoByName(string name)
        {
            return _container.FirstOrDefault(service => service.Name.ToLower().Equals(name.ToLower()) && !service.IsDeprecated);
        }

        public ICollection<ServiceInfo> GetAvailableServices()
        {
            return _container.Where(s => !s.IsDeprecated).ToList();
        }
    }
}