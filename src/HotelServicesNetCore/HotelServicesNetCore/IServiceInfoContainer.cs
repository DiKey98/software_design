using System.Collections.Generic;

namespace HotelServicesNetCore
{
    public interface IServiceInfoContainer
    {
        void AddServiceInfo(ServiceInfo service);
        void RemoveServiceInfo(ServiceInfo service);
        void UpdateService(ServiceInfo oldService, ServiceInfo newService);
        ServiceInfo GetServiceInfoById(string id);
        ServiceInfo GetServiceInfoByName(string name);
        ICollection<ServiceInfo> GetAvailableServices();
    }
}