using System;

namespace Domain.Models.Dto.Login;

public class RegisterDto
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? Dob { get; set; }
        public bool? Gender { get; set; }
    }
