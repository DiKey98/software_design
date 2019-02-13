using System;
using System.Collections.Generic;

namespace HotelServicesLib
{
    public interface IServicesOperations
    {
        void ChangeServiceInfo(ServiceInfo oldService, ServiceInfo newService);
        ICollection<ServiceInfo> GetAvailableServices();
        ICollection<Order> GetOrders(User user = null, DateTime? from = null, DateTime? to = null);
    }
}