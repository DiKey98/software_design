namespace HotelServicesLib
{
    public interface IService
    {
        string Name { get; }
        string Id { get; }
        decimal Cost { get; }
        bool IsPaid { get; set; }
        Client Client { get; }
    }
}