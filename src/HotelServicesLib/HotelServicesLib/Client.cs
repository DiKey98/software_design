using System;

namespace HotelServicesLib
{
    public class Client
    {
        private readonly string _fio;

        public readonly string Id;

        public Client(string fio)
        {
            _fio = fio;
            Id = Guid.NewGuid().ToString();
        }

        public Client(Client client)
        {
            _fio = client._fio;
            Id = Guid.NewGuid().ToString();
        }
    }
}