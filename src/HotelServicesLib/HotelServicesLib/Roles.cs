using System.Collections.Generic;

namespace HotelServicesLib
{
    public class Roles
    {
        public static readonly Dictionary<string, RolesValues> Values = new Dictionary<string, RolesValues>
        {
            { "администратор", RolesValues.Admin },
            { "управляющий", RolesValues.Manager },
            { "клиент", RolesValues.Client }
        };

        public enum RolesValues
        {
            Admin = 0,
            Manager = 1,
            Client = 2
        }
    }
}