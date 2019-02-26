using System;
using System.Linq;
using HotelServicesNetCore;
using Microsoft.EntityFrameworkCore;

namespace ConsoleTestNetCore.Containers.InDB
{
    public class InDbRolesContainer : IRolesContainer
    {
        public Role GetRoleById(string id)
        {
            using (var db = new HotelServicesDbContext())
            {
                return db.Roles.AsNoTracking().FirstOrDefault(r => r.Id == id);
            }
        }

        public Role GetRoleByName(string name)
        {
            using (var db = new HotelServicesDbContext())
            {
                return db.Roles.AsNoTracking().
                    FirstOrDefault(r => string.Equals(r.Name, name, StringComparison.CurrentCultureIgnoreCase));
            }
        }
    }
}