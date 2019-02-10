namespace HotelServicesLib
{
    public interface IServicesContainer
    {
        void AddService(IService service);
        void RemoveService(IService service);
        IService GetServiceById(string id);
    }
}