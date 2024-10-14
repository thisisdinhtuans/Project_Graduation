using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities
{
    public class UserRefreshTokens
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string RefreshToken { get; set; }
        public bool IsActive { get; set; } = true;
    }
}