namespace HotelServicesNetCore
{
    public interface IServicesOperations
    {
        void ChangeServiceInfo(ServiceInfo oldService, ServiceInfo newService);
    }
}