using System;

namespace Domain.Models.Dto.Login;

public class LoginResponDto
{
        public bool Status { get; set; }
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public int Time { get; set; }
        public UserDto? User { get; set; }
}
