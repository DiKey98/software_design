using System;

namespace HotelServicesLib
{
    public interface IService
    {
        string Name { get; }
        string Id { get; }
        decimal Cost { get; }
        bool IsPaid { get; set; }
        DateTime TimeOrder { get; }
        User Client { get; }
    }
}