using System;

namespace Domain.Models.Dto.Login;

public class LoginRequestDto
{
    public string UserName { get; set; }
    public string PassWord { get; set; }
    public bool RememberMe { get; set; }
}
