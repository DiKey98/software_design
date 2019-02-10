using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelServicesLib;

namespace ConsoleTest.Services
{
    class SpaService : IService
    {
        public string Name { get; }
        public string Id { get; }
        public decimal Cost { get; }
        public bool IsPaid { get; set; }
        public Client Client { get; }

        public SpaService(string name, string id, decimal cost, bool isPaid, Client client)
        {
            Name = name;
            Id = id;
            Cost = cost;
            IsPaid = isPaid;
            Client = client;
        }
    }
}
