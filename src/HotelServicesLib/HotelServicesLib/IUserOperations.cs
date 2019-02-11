namespace HotelServicesLib
{
    public interface IUserOperations
    {
        void ChangeUser(User newUser);
        void OrderService(IService service);
        void PayService(IService service);
        void CancelService(IService service);
    }
}