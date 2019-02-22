using System;
using System.Collections.Generic;

namespace HotelServicesNetCore
{
    public interface IOrdersContainer
    {
        void AddOrder(Order order);
        void RemoveOrder(Order order);
        Order GetOrderById(string id);
        ICollection<Order> GetOrders(User user = null, bool paid = true, bool unpaid = true, DateTime? from = null, DateTime? to = null);
    }
}