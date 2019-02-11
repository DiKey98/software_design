using System;

namespace HotelServicesLib
{
    public interface IService
    {
        string Id { get; }
        string Name { get; }
        decimal Cost { get; }
        bool IsPaid { get; set; }
        DateTime TimeOrder { get; }
        User Client { get; }
    }
}