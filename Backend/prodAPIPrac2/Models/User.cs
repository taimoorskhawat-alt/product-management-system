using prodAPIPrac2.Order_models;

namespace prodAPIPrac2.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public string Role { get; set; } = "User";
        public ICollection<Orders> Orders { get; set; } = new List<Orders>();
    }
}
