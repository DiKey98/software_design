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

        public void UpdateService(ServiceInfo oldService, ServiceInfo newService)
        {
            using (var db = new HotelServicesDbContext())
            {
                if (oldService == null || newService == null)
                {
                    return;
                }

                var ord = db.ServiceInfos.FirstOrDefault(o => o.Id == oldService.Id);
                if (ord == null)
                {
                    return;
                }

                ord.IsDeprecated = true;
                db.ServiceInfos.Add(newService);
                db.SaveChanges();
            }

            //using (var db = new HotelServicesDbContext())
            //{
            //    newService.IsDeprecated = false;
            //    db.ServiceInfos.Add(newService);
            //    db.SaveChanges();
            //}
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
                    r => string.Equals(r.Name, name, StringComparison.CurrentCultureIgnoreCase) && !r.IsDeprecated);
            } 
        }

        public ICollection<ServiceInfo> GetAvailableServices()
        {
            using (var db = new HotelServicesDbContext())
            {
                return db.ServiceInfos.Where(s => !s.IsDeprecated).AsNoTracking().ToList();
            }
        }
    }
}