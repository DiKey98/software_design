namespace HotelServicesLib
{
    public class ServiceInfo
    {
        public ServiceInfo(string name, decimal cost)
        {
            Name = name;
            Cost = cost;
        }

        public string Name { get; }
        public decimal Cost { get; }
    }
}