using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelServicesLib;

namespace ConsoleTest.Services
{
    public class Billiards : IService
    {
        public string Id { get; }
        public bool IsPaid { get ; set; }
        public DateTime TimeOrder { get; }
        public User Client { get; }
        public string Name { get; }
        public decimal Cost { get; }
        public TimeSpan Time { get; }

        public Billiards(string name, decimal cost, bool isPaid, User client, DateTime timeOrder, TimeSpan time)
        {
            Id = Guid.NewGuid().ToString();
            IsPaid = isPaid;
            TimeOrder = timeOrder;
            Client = client;
            Name = name;
            Cost = cost;
            Time = time;
        }
    }
}
