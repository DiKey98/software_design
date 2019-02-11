using System.Collections.Generic;

namespace HotelServicesLib
{
    public interface IServicesContainer
    {
        void AddService(IService service);
        void RemoveService(IService service);
        IService GetServiceById(string id);
        ServiceInfo GetServiceInfoByName(string name);
        ICollection<ServiceInfo> GetAllAvailableServices();
        ICollection<IService> GetAllServices(User user = null);
        ICollection<IService> GetPaidServices(User user = null);
        ICollection<IService> GetUnPaidServices(User user = null);
    }
}