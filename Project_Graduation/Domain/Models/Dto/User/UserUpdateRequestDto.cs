using System;

namespace Domain.Models.Dto.User;

public class UserUpdateRequestDto
{
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public bool? Gender { get; set; }
        public string? PhoneNumber { get; set; }
        public string? CCCD { get; set; }
        public DateTime Dob { get; set; }
    public int RestaurantId { get; set; }
    public string? Role { get; set; }
}
