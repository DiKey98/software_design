using System.Collections.Generic;

namespace HotelServicesLib
{
    public class Roles
    {
        private static readonly Dictionary<string, RolesValues> Values = new Dictionary<string, RolesValues>
        {
            { "администратор", RolesValues.Admin },
            { "управляющий", RolesValues.Manager },
            { "клиент", RolesValues.Client }
        };

        public enum RolesValues
        {
            ErrorRole = -1,
            Admin = 0,
            Manager = 1,
            Client = 2
        }

        public static RolesValues StringToRole(string s)
        {
            if (s == null)
            {
                return RolesValues.ErrorRole;
            }
            return !Values.ContainsKey(s.ToLower()) ? RolesValues.ErrorRole : Values[s.ToLower()];
        }
    }
}