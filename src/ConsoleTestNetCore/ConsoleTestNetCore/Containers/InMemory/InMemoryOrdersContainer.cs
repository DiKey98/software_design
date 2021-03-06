﻿using System;
using System.Collections.Generic;
using System.Linq;
using HotelServicesNetCore;

namespace ConsoleTestNetCore.Containers.InMemory
{
    public class InMemoryOrdersContainer: IOrdersContainer
    {
        //private static InMemoryOrdersContainer _ordersContainer;

        private readonly List<Order> _container;

        public InMemoryOrdersContainer()
        {
            _container = new List<Order>();
        }

        //public static InMemoryOrdersContainer GetInstance()
        //{
        //    return _ordersContainer ?? (_ordersContainer = new InMemoryOrdersContainer());
        //}

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

        public void UpdateOrder(Order oldOrder, Order newOrder)
        {
            var ord = _container.FirstOrDefault(o => o.Id == oldOrder.Id);
            if (ord == null)
            {
                return;
            }
            _container.Remove(oldOrder);
            _container.Add(newOrder);
        }

        public Order GetOrderById(string id)
        {
            return _container.FirstOrDefault(order => order.Id.Equals(id));
        }

        public ICollection<Order> GetOrders(User user = null, bool paid = true, bool unpaid = true, DateTime? from = null, DateTime? to = null)
        {
            if (!paid && !unpaid)
            {
                return null;
            }

            var result = user == null
                ? _container.Where(order => true)
                : _container.Where(order => order.User.Equals(user));

            if (!(paid && unpaid))
            {
                result = paid ? result.Where(order => order.IsPaid) : result.Where(order => !order.IsPaid);
            }

            if (from != null)
            {
                result = result.Where(order => order.OrderDate >= from);
            }

            if (to != null)
            {
                result = result.Where(order => order.OrderDate <= to);
            }

            return result.ToList();
        }
    }
}