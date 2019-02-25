using System.Collections.Generic;

namespace HotelServicesNetCore
{
    public class ServiceInfo
    {
        public ServiceInfo()
        {
            Orders = new List<Order>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public decimal CostPerUnit { get; set; }
        public string Measurement { get; set; }

        public List<Order> Orders { get; set; }
    }
}