using System;

namespace Domain.Models.Dto.Staff;

public class StaffCreateDto
{
        public string Email { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string PassWord { get; set; }
        public DateTime Dob { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
        public bool? Gender { get; set; }
        public string? CCCD { get; set; }
        public int RestaurantID { get; set; }
}
