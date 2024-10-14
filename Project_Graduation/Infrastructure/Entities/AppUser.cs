using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Entities
{
    public class AppUser: IdentityUser<Guid>
    {
        public string? FullName { get; set; }
        public DateTime Dob { get; set; }
        public string? RefreshToken { get; set; }
        public int RestaurantID { get; set; }
        public int Status { get; set; }
        public bool? Gender { get; set; }
        public string? CCCD { get; set; }
        public string? RecoveryToken { get; set; }
    }
}