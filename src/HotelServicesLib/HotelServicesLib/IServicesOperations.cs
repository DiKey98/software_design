namespace HotelServicesLib
{
    public interface IServicesOperations
    {
        void ChangeServiceInfo(ServiceInfo oldService, ServiceInfo newService);
    }
}