using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Entities
{
    public class Role : IdentityRole<Guid>
    {
        // [Key]
        // public Guid RoleId {get; set;}
        public string? Description { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}