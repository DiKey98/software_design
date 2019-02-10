using System;

namespace HotelServicesLib
{
    public interface IService
    {
        string Name { get; }
        string Id { get; }
        bool IsPaid { get; set; }
        DateTime TimeOrder { get; }
        User Client { get; }
    }
}