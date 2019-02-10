using System.Collections.Generic;

namespace HotelServicesLib
{
    public interface IServicesContainer
    {
        void AddService(IService service);
        void RemoveService(IService service);
        IService GetServiceById(string id);
        ICollection<IService> GetAllServices();
        ICollection<IService> GetPaidServices(Client client = null);
        ICollection<IService> GetUnPaidServices(Client client = null);
    }
}