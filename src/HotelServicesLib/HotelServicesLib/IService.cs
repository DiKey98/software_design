namespace HotelServicesLib
{
    public interface IService
    {
        string Name { get; }
        string Id { get; }
        bool IsPaid { get; }
        Client Client { get; }
    }
}