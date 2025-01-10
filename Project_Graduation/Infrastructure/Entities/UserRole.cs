using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Entities
{
    public class UserRole : IdentityUserRole<Guid>
    {
        public User User { get; set; }
        public Role Role { get; set; }
    }
}