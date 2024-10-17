using System;
using Domain.Features;
using Domain.Models.Dto.Login;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services;

public class AccountService: IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly TokenService _tokenService;

        public AccountService(UserManager<AppUser> userManager, TokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<UserDto> LoginAsync(LoginRequestDto loginDto)
        {
            // Find user by username
            var user = await _userManager.FindByNameAsync(loginDto.UserName);
            
            // Check if user exists and password is valid
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.PassWord))
                return null;

            // Return user details along with JWT token
            return new UserDto
            {
                UserName = user.UserName,        // Username
                FullName = user.FullName,        // Full Name
                Email = user.Email,              // Email
                PhoneNumber = user.PhoneNumber,  // Phone number (if needed)
                Token = await _tokenService.GenerateToken(user), // JWT Token
            };
        }



    public async Task<IdentityResult> RegisterAsync(RegisterDto registerDto)
{
    // Tạo đối tượng AppUser với các thuộc tính từ RegisterDto
    var user = new AppUser 
    { 
        UserName = registerDto.UserName, 
        Email = registerDto.Email,
        PhoneNumber = registerDto.PhoneNumber,
        FullName = registerDto.FullName,
        Dob = registerDto.Dob,
        Gender = registerDto.Gender
    };

    // Thực hiện tạo user với password
    var result = await _userManager.CreateAsync(user, registerDto.PassWord);

    // Nếu thành công, thêm vào role "Customer"
    if (result.Succeeded)
    {
        await _userManager.AddToRoleAsync(user, "Customer");
    }
    
    return result;
}

}