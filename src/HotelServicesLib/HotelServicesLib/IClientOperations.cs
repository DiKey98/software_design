namespace HotelServicesLib
{
    public interface IClientOperations
    {
        void ChangeClient(Client oldClient, Client newClient);
        void OrderService(IService service);
        void PayService(IService service);
        void CancelService(IService service);
    }
}