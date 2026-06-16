using prodAPIPrac2.data;
using prodAPIPrac2.interfaces;
using prodAPIPrac2.Models;

namespace prodAPIPrac2.services
{
    public class UserService:IuserService
    {
        private readonly appdbcontext _context;

        public UserService(appdbcontext context)
        {
            _context = context;
        }

        public List<Userdto> GetAllUsers()
        {
            return _context.users.Select(u => new Userdto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                Role = u.Role
            }).ToList();
        }

        public bool UpdateUserRole(int userId, string role)
        {
            var user = _context.users.Find(userId);
            if (user == null) return false;

            user.Role = role;
            _context.SaveChanges();
            return true;
        }

    }
}
