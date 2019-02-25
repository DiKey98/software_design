namespace HotelServicesNetCore
{
    public interface IRolesContainer
    {
        Role GetRoleById(string id);
        Role GetRoleByName(string name);
    }
}