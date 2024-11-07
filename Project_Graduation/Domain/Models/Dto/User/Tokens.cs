using System;

namespace Domain.Models.Dto.User;

public class Tokens
{
    public string? Access_Token { get; set; }
    public string? Refresh_Token { get; set; }
}
