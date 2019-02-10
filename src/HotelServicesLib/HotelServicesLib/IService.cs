namespace HotelServicesLib
{
    public interface IService
    {
        string Name { get; }
        string Id { get; }
        bool IsPaid { get; set; }
        Client Client { get; }
    }
}