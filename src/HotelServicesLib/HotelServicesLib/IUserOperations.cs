namespace HotelServicesLib
{
    public interface IUserOperations
    {
        void ChangeUser(User oldUser, User newUser);
        void OrderService(IService service);
        void PayService(IService service);
        void CancelService(IService service);
    }
}