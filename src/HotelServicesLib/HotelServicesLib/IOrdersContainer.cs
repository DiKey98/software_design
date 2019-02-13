namespace HotelServicesLib
{
    public interface IOrdersContainer
    {
        void AddOrder(Order order);
        void RemoveOrder(Order order);
        Order GetOrderById(string id);
    }
}