﻿using System;
using System.Collections.Generic;
using System.Linq;
using HotelServicesNetCore;
using Microsoft.EntityFrameworkCore;

namespace ConsoleTestNetCore.Containers.InDB
{
    public class InDbOrdersContainer : IOrdersContainer
    {
        public void AddOrder(Order order)
        {
            if (order == null)
            {
                return;
            }

            using (var db = new HotelServicesDbContext())
            {
                db.Orders.Add(order);
                db.Entry(order.Service).State = EntityState.Unchanged;
                db.Entry(order.User).State = EntityState.Unchanged;
                db.Entry(order.User.Role).State = EntityState.Unchanged;
                db.SaveChanges();
            }
        }

        public void RemoveOrder(Order order)
        {
            if (order == null)
            {
                return;
            }

            using (var db = new HotelServicesDbContext())
            {
                db.Orders.Remove(order);
                db.SaveChanges();
            }        
        }

        public void UpdateOrder(Order oldOrder, Order newOrder)
        {
            using (var db = new HotelServicesDbContext())
            {
                if (oldOrder == null || newOrder == null)
                {
                    return;
                }

                var ord = GetOrderById(oldOrder.Id);
                if (ord == null)
                {
                    return;
                }

                ord.Id = newOrder.Id;
                ord.Cost = newOrder.Cost;
                ord.IsPaid = newOrder.IsPaid;
                ord.OrderDate = newOrder.OrderDate;
                ord.Service = newOrder.Service;
                ord.Units = newOrder.Units;
                ord.User = newOrder.User;

                db.Entry(ord.Service).State = EntityState.Unchanged;
                db.Entry(ord.User).State = EntityState.Unchanged;
                db.Entry(ord).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public Order GetOrderById(string id)
        {
            using (var db = new HotelServicesDbContext())
            {
                return
                (from order in db.Orders
                    join service in db.ServiceInfos on order.ServiceId equals service.Id
                    join user in db.Users on order.UserId equals user.Id
                    where order.Id == id
                    select new Order
                    {
                        Id = order.Id,
                        Cost = order.Cost,
                        IsPaid = order.IsPaid,
                        OrderDate = order.OrderDate,
                        Service = service,
                        User = user,
                        Units = order.Units
                    }).AsNoTracking().FirstOrDefault();
            }
        }

        public ICollection<Order> GetOrders(User user = null, bool paid = true, bool unpaid = true, DateTime? from = null,
            DateTime? to = null)
        {
            if (!paid && !unpaid)
            {
                return null;
            }
            using (var db = new HotelServicesDbContext())
            {
                var result = user == null
                    ? (from order in db.Orders
                        join service in db.ServiceInfos on order.ServiceId equals service.Id
                        join usr in db.Users on order.UserId equals usr.Id
                        select new Order
                        {
                            Id = order.Id,
                            Cost = order.Cost,
                            IsPaid = order.IsPaid,
                            OrderDate = order.OrderDate,
                            Service = service,
                            User = usr,
                            Units = order.Units
                        }).AsNoTracking()
                    : (from order in db.Orders
                        join service in db.ServiceInfos on order.ServiceId equals service.Id
                        join usr in db.Users on order.UserId equals usr.Id
                        where order.UserId == user.Id
                        select new Order
                        {
                            Id = order.Id,
                            Cost = order.Cost,
                            IsPaid = order.IsPaid,
                            OrderDate = order.OrderDate,
                            Service = service,
                            User = usr,
                            Units = order.Units
                        }).AsNoTracking();

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
}