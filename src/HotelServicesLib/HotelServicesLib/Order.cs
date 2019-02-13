using System;

namespace HotelServicesLib
{
    public class Order
    {
        public string Id { get; }
        public ServiceInfo Service { get; }
        public decimal Cost { get; }
        public uint Units { get; }
        public bool IsPaid { get; set; }
        public DateTime OrderDate { get; }
        public User Client { get; }

        public Order(ServiceInfo service, uint units, bool isPaid, DateTime orderDate, User client)
        {
            Id = Guid.NewGuid().ToString();
            Service = service;
            Cost = service.CostPerUnit * units;
            IsPaid = isPaid;
            OrderDate = orderDate;
            Client = client;
            Units = units;
        }
    }
}