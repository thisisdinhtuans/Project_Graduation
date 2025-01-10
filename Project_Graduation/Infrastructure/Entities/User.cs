using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Entities
{
    public class User: IdentityUser<Guid>
    {
        // [Key]
        // public Guid UserId { get; set; }
        public string? FullName { get; set; }
        public DateTime? Dob { get; set; }
        public string? RefreshToken { get; set; }
        public int? RestaurantID { get; set; }
        public int? Status { get; set; }
        public bool? Gender { get; set; }
        public string? CCCD { get; set; }
        public string? RecoveryToken { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}