namespace HotelServicesLib
{
    public interface IServiceOperations
    {
        void ChangeService(IService oldService, IService newService);
    }
}