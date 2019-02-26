using System;
using System.Collections.Generic;
using System.Linq;
using HotelServicesNetCore;
using Microsoft.EntityFrameworkCore;

namespace ConsoleTestNetCore.Containers.InDB
{
    public class InDbOrdersContainer : IOrdersContainer
    {
        private readonly HotelServicesDbContext _dbContext;

        public InDbOrdersContainer(HotelServicesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddOrder(Order order)
        {
            if (order == null)
            {
                return;
            }
            _dbContext.Orders.Add(order);
            _dbContext.Entry(order.Service).State = EntityState.Unchanged;
            _dbContext.Entry(order.User).State = EntityState.Unchanged;
            _dbContext.SaveChanges();
        }

        public void RemoveOrder(Order order)
        {
            if (order == null)
            {
                return;
            }
            _dbContext.Orders.ToList().Remove(order);
            _dbContext.Entry(order.Service).State = EntityState.Unchanged;
            _dbContext.Entry(order.User).State = EntityState.Unchanged;
            _dbContext.SaveChanges();
        }

        public void UpdateOrder(Order oldOrder, Order newOrder)
        {
            var ord = _dbContext.Set<Order>()
                .Local
                .FirstOrDefault(o => o.Id == oldOrder.Id);
           
            if (ord == null)
            {

                ord = GetOrderById(oldOrder.Id);
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

                _dbContext.Entry(ord).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return;
            }

            _dbContext.Entry(ord).State = EntityState.Detached;
            _dbContext.Entry(newOrder).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public Order GetOrderById(string id)
        {
            return
                (from order in _dbContext.Orders
                 join service in _dbContext.ServiceInfos on order.ServiceId equals service.Id
                 join user in _dbContext.Users on order.UserId equals user.Id
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
                 }).FirstOrDefault();
        }

        public ICollection<Order> GetOrders(User user = null, bool paid = true, bool unpaid = true, DateTime? from = null,
            DateTime? to = null)
        {
            if (!paid && !unpaid)
            {
                return null;
            }

            var result = user == null
                ? (from order in _dbContext.Orders
                   join service in _dbContext.ServiceInfos on order.ServiceId equals service.Id
                   join usr in _dbContext.Users on order.UserId equals usr.Id
                   select new Order
                   {
                       Id = order.Id,
                       Cost = order.Cost,
                       IsPaid = order.IsPaid,
                       OrderDate = order.OrderDate,
                       Service = service,
                       User = usr,
                       Units = order.Units
                   })
                : (from order in _dbContext.Orders
                   join service in _dbContext.ServiceInfos on order.ServiceId equals service.Id
                   join usr in _dbContext.Users on order.UserId equals usr.Id
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
                   });

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