using System;

namespace HotelServicesLib
{
    public interface IService: IServiceInfo
    {
        string Id { get; }
        bool IsPaid { get; set; }
        DateTime TimeOrder { get; }
        User Client { get; }
    }
}