using HotelServicesLib;

namespace ConsoleTest.ServicesInfo
{
    public class SpaServiceInfo: IServiceInfo
    {
        public string Name { get; }
        public decimal Cost { get; }

        public SpaServiceInfo(string name, decimal cost)
        {
            Name = name;
            Cost = cost;
        }
    }
}