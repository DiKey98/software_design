using System.Collections.Generic;
using System.Linq;
using HotelServicesLib;

namespace ConsoleTest.Containers
{
    public class InMemoryOrdersContainer: IOrdersContainer
    {
        private static InMemoryOrdersContainer _ordersContainer;

        private readonly List<Order> _container;

        private InMemoryOrdersContainer()
        {
            _container = new List<Order>();
        }

        public static InMemoryOrdersContainer GetInstance()
        {
            return _ordersContainer ?? (_ordersContainer = new InMemoryOrdersContainer());
        }

        public void AddOrder(Order order)
        {
            if (order != null)
            {
                _container.Add(order);
            }
        }

        public void RemoveOrder(Order order)
        {
            _container.Remove(order);
        }

        public Order GetOrderById(string id)
        {
            return _container.FirstOrDefault(order => order.Id.Equals(id));
        }
    }
}