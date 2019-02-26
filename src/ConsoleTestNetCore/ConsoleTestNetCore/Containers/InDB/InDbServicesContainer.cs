using System;
using System.Collections.Generic;
using System.Linq;
using HotelServicesNetCore;
using Microsoft.EntityFrameworkCore;

namespace ConsoleTestNetCore.Containers.InDB
{
    public class InDbServicesContainer : IServiceInfoContainer
    {
        public void AddServiceInfo(ServiceInfo service)
        {
            if (service == null)
            {
                return;
            }

            using (var db = new HotelServicesDbContext())
            {
                db.ServiceInfos.Add(service);
                db.SaveChanges();
            }
        }

        public void RemoveServiceInfo(ServiceInfo service)
        {
            if (service == null)
            {
                return;
            }

            using (var db = new HotelServicesDbContext())
            {
                db.ServiceInfos.Remove(service);
                db.SaveChanges();
            }
        }

        public ServiceInfo GetServiceInfoById(string id)
        {
            using (var db = new HotelServicesDbContext())
            {
                return db.ServiceInfos.AsNoTracking().FirstOrDefault(s => s.Id == id);
            }
                
        }

        public ServiceInfo GetServiceInfoByName(string name)
        {
            using (var db = new HotelServicesDbContext())
            {
                return db.ServiceInfos.AsNoTracking().FirstOrDefault(
                    r => string.Equals(r.Name, name, StringComparison.CurrentCultureIgnoreCase));
            } 
        }

        public ICollection<ServiceInfo> GetAvailableServices()
        {
            using (var db = new HotelServicesDbContext())
            {
                return db.ServiceInfos.AsNoTracking().ToList();
            }
        }
    }
}