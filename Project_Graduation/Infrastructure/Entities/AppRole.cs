using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Entities
{
    public class AppRole : IdentityRole<Guid>
    {
        public string? Description { get; set; }
    }
}