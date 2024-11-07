using System;

namespace Domain.Models.Dto.User;

public class RenewPassword
{
    public string PassWord { get; set; }
    public string TokenRenew { get; set; }
}
