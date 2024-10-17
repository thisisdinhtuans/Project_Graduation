using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services;

public class TokenService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _config;

        public TokenService(UserManager<AppUser> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }
        public async Task<string> GenerateToken(AppUser user)
        {
            //Tạo claims về thông tin người dùng
            var claims = new List<Claim>
{
                new Claim(ClaimTypes.Email, user.Email),            // Email
                new Claim(ClaimTypes.Name, user.FullName),          // Full name
                new Claim(ClaimTypes.NameIdentifier, user.UserName) // User name
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            //tạo khóa bí mật
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWTSettings:TokenKey"]));
            //tạo mã ký và mã hóa
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            //thiết lập cấu hình token
            var tokenOptions = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                //thời gian hết hạn của token
                expires: DateTime.Now.AddDays(7),
                signingCredentials: creds
            );
            //trả về token dưới dạng chuỗi
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }
    }
