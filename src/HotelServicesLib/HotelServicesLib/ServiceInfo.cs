namespace HotelServicesLib
{
    public class ServiceInfo
    {
        public ServiceInfo(string name, decimal cost, string measurement)
        {
            Name = name;
            Cost = cost;
            Measurement = measurement;
        }

        public string Name { get; }
        public decimal Cost { get; }
        public string Measurement { get; }
    }
}