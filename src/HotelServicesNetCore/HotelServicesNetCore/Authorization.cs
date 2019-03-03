namespace HotelServicesNetCore
{
    public class Authorization
    {
        public string Id { get; set; }
        public string SessionId { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}