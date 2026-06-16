using prodAPIPrac2.Models;

namespace prodAPIPrac2.interfaces
{
    public interface IuserService
    {
        List<Userdto> GetAllUsers();
        bool UpdateUserRole(int userId, string role);
    }
}
