using System;
using System.Collections.Generic;
using System.Linq;
using HotelServicesNetCore;

namespace ConsoleTestNetCore.Containers.InDB
{
    public class InDbServicesContainer : IServiceInfoContainer
    {
        private readonly HotelServicesDbContext _dbContext;

        public InDbServicesContainer(HotelServicesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddServiceInfo(ServiceInfo service)
        {
            if (service == null)
            {
                return;
            }
            _dbContext.ServiceInfos.Add(service);
            _dbContext.SaveChanges();
        }

        public void RemoveServiceInfo(ServiceInfo service)
        {
            if (service == null)
            {
                return;
            }
            _dbContext.ServiceInfos.Remove(service);
            _dbContext.SaveChanges();
        }

        public ServiceInfo GetServiceInfoById(string id)
        {
            return _dbContext.ServiceInfos.FirstOrDefault(s => s.Id == id);
        }

        public ServiceInfo GetServiceInfoByName(string name)
        {
            return _dbContext.ServiceInfos.FirstOrDefault(
                r => string.Equals(r.Name, name, StringComparison.CurrentCultureIgnoreCase));
        }

        public ICollection<ServiceInfo> GetAvailableServices()
        {
            return _dbContext.ServiceInfos.ToList();
        }
    }
}