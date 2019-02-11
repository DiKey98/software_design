using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelServicesLib;

namespace ConsoleTest.Services
{
    public class SpaService : IService
    {
        public string Name { get; }
        public string Id { get; }
        public decimal Cost { get; }
        public bool IsPaid { get; set; }
        public DateTime TimeOrder { get; }
        public User Client { get; }

        public SpaService(string name, decimal cost, bool isPaid, User client, DateTime timeOrder)
        {
            Name = name;
            Id = Guid.NewGuid().ToString();
            Cost = cost;
            IsPaid = isPaid;
            Client = client;
            TimeOrder = timeOrder;
        }
    }
}
