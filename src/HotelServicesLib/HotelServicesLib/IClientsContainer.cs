namespace HotelServicesLib
{
    public interface IClientsContainer
    {
        void AddClient(Client client);
        void RemoveClient(Client client);
        Client GetClientById(string id);
    }
}