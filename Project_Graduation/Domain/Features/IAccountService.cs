using System;
using Domain.Models.Dto.Login;
using Microsoft.AspNetCore.Identity;

namespace Domain.Features;

public interface IAccountService
{
    Task<UserDto> LoginAsync(LoginRequestDto loginDto);
    Task<IdentityResult> RegisterAsync(RegisterDto registerDto);
}
