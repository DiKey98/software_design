using System.Collections.Generic;

namespace HotelServicesNetCore
{
    public class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Fio { get; set; }
        public string Id { get; set; }

        public string RoleId { get; set; }
        public Role Role { get; set; }

        public List<Order> Order { get; set; }

        public Authorization Authorization { get; set; }
    }
}