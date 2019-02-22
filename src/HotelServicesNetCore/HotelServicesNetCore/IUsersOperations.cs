namespace HotelServicesNetCore
{
    public interface IUsersOperations
    {
        void ChangeUser(User oldUser, User newUser);
        void OrderService(User user, string name, uint units);
        void PayService(User user, string id);
        void CancelService(User user, string id);
    }
}