using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelServicesLib;

namespace ConsoleTest.Services
{
    public class Alcohol : IService
    {
        public string Id { get; }
        public bool IsPaid { get; set; }
        public string Name { get; }
        public decimal Cost { get; }
        public decimal Bulk { get; }
        public uint Quantity { get; } 
        public DateTime TimeOrder { get; }
        public User Client { get; }


        public Alcohol(string name, decimal cost, bool isPaid, User client, DateTime timeOrder, decimal bulk, uint quantity)
        {
            Id = Guid.NewGuid().ToString(); 
            IsPaid = isPaid;
            TimeOrder = timeOrder;
            Client = client;
            Name = name;
            Cost = cost;
            Bulk = bulk;
            Quantity = quantity;
        }
    }
}
