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
            var user = await _userManager.FindByNameAsync(loginDto.UserName);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.PassWord))
                return null;

            return new UserDto
            {
                Email = user.Email,
                Token = await _tokenService.GenerateToken(user),
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