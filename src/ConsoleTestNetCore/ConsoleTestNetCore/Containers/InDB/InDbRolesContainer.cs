using System;
using System.Linq;
using HotelServicesNetCore;

namespace ConsoleTestNetCore.Containers.InDB
{
    public class InDbRolesContainer : IRolesContainer
    {
        private readonly HotelServicesDbContext _dbContext;

        public InDbRolesContainer(HotelServicesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Role GetRoleById(string id)
        {
            return _dbContext.Roles.FirstOrDefault(r => r.Id == id);
        }

        public Role GetRoleByName(string name)
        {
            return _dbContext.Roles.FirstOrDefault(r => string.Equals(r.Name, name, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}