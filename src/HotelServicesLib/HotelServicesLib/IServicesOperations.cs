using System;
using System.Collections.Generic;

namespace HotelServicesLib
{
    public interface IServicesOperations
    {
        void ChangeServiceInfo(ServiceInfo oldService, ServiceInfo newService);
        ICollection<Order> GetOrders(User user = null, bool paid = true, bool unpaid = true, DateTime? from = null, DateTime? to = null);
    }
}