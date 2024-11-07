using System;

namespace Domain.Models.Dto.User;

public class EmailChecked
{
    public string Email { get; set; }
    public string ClientUrl { get; set; }
    public string TokenRenew { get; set; }
    public string UserId { get; set; }
}
