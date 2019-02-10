using System.Collections.Generic;

namespace HotelServicesLib
{
    public interface IServicesContainer
    {
        void AddService(IService service);
        void RemoveService(IService service);
        IService GetServiceById(string id);
        ICollection<IService> GetAllServices();
        ICollection<IService> GetPaidServices(User user = null);
        ICollection<IService> GetUnPaidServices(User user = null);
    }
}