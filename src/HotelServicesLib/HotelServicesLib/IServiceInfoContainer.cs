namespace HotelServicesLib
{
    public interface IServiceInfoContainer
    {
        void AddServiceInfo(ServiceInfo service);
        void RemoveServiceInfo(ServiceInfo service);
        ServiceInfo GetServiceInfoById(string id);
        ServiceInfo GetServiceInfoByName(string name);
    }
}