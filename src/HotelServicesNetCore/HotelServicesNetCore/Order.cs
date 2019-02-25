using System;

namespace HotelServicesNetCore
{
    public class Order
    {
        public string Id { get; set; }
        public decimal Cost { get; set; }
        public uint Units { get; set; }
        public bool IsPaid { get; set; }
        public DateTime OrderDate { get; set; }
    
        public string ServiceId { get; set; }
        public ServiceInfo Service { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}