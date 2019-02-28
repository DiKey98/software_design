﻿using System;
using Castle.Core;
using HotelServicesNetCore;

namespace WebServer.Services
{
    [Interceptor("consoleLogger")]
    public class UserOperations : IUsersOperations
    {
        private readonly IOrdersContainer _ordersContainer;
        private readonly IServiceInfoContainer _serviceInfoContainer;
        private readonly IUsersContainer _usersContainer;

        public UserOperations(IUsersContainer usersContainer, IOrdersContainer ordersContainer,
            IServiceInfoContainer serviceInfoContainer)
        {
            _usersContainer = usersContainer;
            _ordersContainer = ordersContainer;
            _serviceInfoContainer = serviceInfoContainer;
        }

        public void ChangeUser(User oldUser, User newUser)
        {
            var tmpUser = _usersContainer.GetUserById(oldUser.Id);
            if (tmpUser == null)
                return;
            _usersContainer.RemoveUser(tmpUser);
            _usersContainer.AddUser(newUser);
        }

        public void OrderService(User user, string name, uint units)
        {
            var serviceInfo = _serviceInfoContainer.GetServiceInfoByName(name);
            var order = new Order
            {
                Id = Guid.NewGuid().ToString(),
                Service = serviceInfo,
                Units = units,
                IsPaid = false,
                OrderDate = DateTime.Now,
                Cost = serviceInfo.CostPerUnit * units,
                User = user
            };

            _ordersContainer.AddOrder(order);
        }

        public void PayService(User user, string id)
        {
            var order = _ordersContainer.GetOrderById(id);
            if (order == null || order.IsPaid)
                return;

            var newOrder = order;
            newOrder.IsPaid = true;
            _ordersContainer.UpdateOrder(order, newOrder);
        }

        public void CancelService(User user, string id)
        {
            var order = _ordersContainer.GetOrderById(id);
            if (order == null || order.IsPaid)
                return;
            _ordersContainer.RemoveOrder(order);
        }
    }
}