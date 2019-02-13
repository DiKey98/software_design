﻿using System;
using HotelServicesLib;

namespace ConsoleTest.Operations
{
    public class InMemoryUserOperations: IUsersOperations
    {
        private static InMemoryUserOperations _operations;

        private readonly IUsersContainer _usersContainer;
        private readonly IOrdersContainer _ordersContainer;
        private readonly IServiceInfoContainer _serviceInfoContainer;

        private InMemoryUserOperations(IUsersContainer usersContainer, IOrdersContainer ordersContainer, 
            IServiceInfoContainer serviceInfoContainer)
        {
            _usersContainer = usersContainer;
            _ordersContainer = ordersContainer;
            _serviceInfoContainer = serviceInfoContainer;
        }

        public static InMemoryUserOperations GetInstance(IUsersContainer container, IOrdersContainer servicesContainer,
            IServiceInfoContainer serviceInfoContainer)
        {
            return _operations ?? (_operations = new InMemoryUserOperations(container, servicesContainer, serviceInfoContainer));
        }

        public void ChangeUser(User oldUser, User newUser)
        {
            var tmpUser = _usersContainer.GetUserById(oldUser.Id);
            if (tmpUser == null)
            {
                return;
            }
            _usersContainer.RemoveUser(tmpUser);
            _usersContainer.AddUser(newUser);
        }

        public void OrderService(User user, string name, uint units)
        {
            var serviceInfo = _serviceInfoContainer.GetServiceInfoByName(name);
            var order = new Order(serviceInfo, units, false, DateTime.Now, user);
            _ordersContainer.AddOrder(order);
        }

        public void PayService(User user, string id)
        {
            var order = _ordersContainer.GetOrderById(id);
            if (order == null || order.IsPaid)
            {
                return;
            }
            _ordersContainer.RemoveOrder(order);
            order.IsPaid = true;
            _ordersContainer.AddOrder(order);
        }

        public void CancelService(User user, string id)
        {
            var order = _ordersContainer.GetOrderById(id);
            if (order == null || order.IsPaid)
            {
                return;
            }
            _ordersContainer.RemoveOrder(order);
        }
    }
}