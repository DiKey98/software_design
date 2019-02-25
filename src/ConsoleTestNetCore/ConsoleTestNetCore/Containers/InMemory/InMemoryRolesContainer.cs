using System;
using System.Collections.Generic;
using System.Linq;
using HotelServicesNetCore;

namespace ConsoleTestNetCore.Containers.InMemory
{
    public class InMemoryRolesContainer : IRolesContainer
    {
        private readonly List<Role> _roles;

        public InMemoryRolesContainer(IEnumerable<Role> roles = null)
        {
            if (roles == null)
            {
                _roles = new List<Role>();
                return;
            }
            _roles = roles as List<Role>;
        }

        public Role GetRoleById(string id)
        {
            return _roles.FirstOrDefault(r => r.Id == id);
        }

        public Role GetRoleByName(string name)
        {
            return _roles.FirstOrDefault(r => string.Equals(r.Name, name, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}