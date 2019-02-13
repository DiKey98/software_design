using System;
using System.Collections.Generic;
using System.Linq;
using HotelServicesLib;

namespace ConsoleTest.Operations
{
    public class InMemoryServiceOperations: IServicesOperations
    {
        private static InMemoryServiceOperations _serviceOperations;

        private readonly List<ServiceInfo> _servicesContainer;
        private readonly List<Order> _ordersContainer;

        private InMemoryServiceOperations(IEnumerable<ServiceInfo> servicesContainer, IEnumerable<Order> ordersContainer)
        {
            _ordersContainer = ordersContainer as List<Order>;
            _servicesContainer = servicesContainer as List<ServiceInfo>;
        }

        public static InMemoryServiceOperations GetInstance(IEnumerable<ServiceInfo> servicesContainer, IEnumerable<Order> ordersContainer)
        {
            return _serviceOperations ?? (_serviceOperations = new InMemoryServiceOperations(servicesContainer, ordersContainer));
        }

        public void ChangeServiceInfo(ServiceInfo oldService, ServiceInfo newService)
        {
            var tmpService = _servicesContainer.FirstOrDefault(service => service.Name == oldService.Name);
            if (tmpService == null)
            {
                return;
            }
            _servicesContainer.Remove(tmpService);
            _servicesContainer.Add(newService);
        }

        public ICollection<ServiceInfo> GetAvailableServices()
        {
            return _servicesContainer.GetRange(0, _servicesContainer.Count);
        }

        public ICollection<Order> GetOrders(User user = null, DateTime? from = null, DateTime? to = null)
        {
            var result = user == null 
                ? _ordersContainer.Where(order => true)
                : _ordersContainer.Where(order => order.Client.Equals(user));

            if (from != null)
            {
                result = result.Where(order => order.OrderDate >= from);
            }

            if (to != null)
            {
                result = result.Where(order => order.OrderDate <= to);
            }

            return result.ToList();
        }
    }
}