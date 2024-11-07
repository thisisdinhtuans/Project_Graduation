using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Domain.Models.Common;
using Domain.Models.Common.ApiResult;
using Domain.Models.Dto.Login;
using Domain.Models.Dto.Role;
using Domain.Models.Dto.User;
using Infrastructure.Data;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services.UserService;

public class UserService : IUserService
{
        private readonly Project_Graduation_Context _dbcontext;
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;

        public UserService(IConfiguration configuration, Project_Graduation_Context dbcontext, UserManager<AppUser> userManager)
        {
            _configuration = configuration;
            _dbcontext = dbcontext;
            _userManager = userManager;
        }
        public async Task<bool> Delete(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return false;
            }
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }

        public async Task<UserRequestDto> GetById(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new UserRequestDto();
            }
            var roles = await _userManager.GetRolesAsync(user);
            //var userOperations = await _userOperationRepository.GetByCondition(x => x.UserId == user.Id);
            //var operations = await _operationRepository.GetAll();
            var uservm = new UserRequestDto()
            {
                Id=user.Id,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FullName = user.FullName,
                UserName = user.UserName,
                Roles = roles,
                Dob = user.Dob,
                Gender = user.Gender,
                CCCD=user.CCCD,
                RestaurantID=user.RestaurantID
                
                //Opes = (userOperations != null && operations != null)
                //    ? userOperations.Join(operations, x1 => x1.OperationId, x2 => x2.Id, (x1, x2) => x2.Name).ToList()
                //    : new List<string>()
            };
            return uservm;
        }

        public async Task<bool> RoleAssign(Guid id, RoleAssignRequestDto request)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return false;
            }
            var removedRoles = request.Roles.Where(x => x.Selected == false).Select(x => x.Name).ToList();
            foreach (var roleName in removedRoles)
            {
                if (await _userManager.IsInRoleAsync(user, roleName) == true)
                {
                    await _userManager.RemoveFromRoleAsync(user, roleName);
                }
            }
            await _userManager.RemoveFromRolesAsync(user, removedRoles);

            var addedRoles = request.Roles.Where(x => x.Selected).Select(x => x.Name).ToList();
            foreach (var roleName in addedRoles)
            {
                if (await _userManager.IsInRoleAsync(user, roleName) == false)
                {
                    await _userManager.AddToRoleAsync(user, roleName);
                }
            }

            return true;
        }

    public async Task<bool> Update(Guid id, UserUpdateRequestDto request)
    {
        // Tìm người dùng dựa trên Id
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user == null)
        {
            return false; 
        }

        // Kiểm tra xem email có bị trùng lặp với người dùng khác không
        var emailExists = _userManager.Users.Any(x => x.Id != id && x.Email == request.Email);
        if (emailExists)
        {
            return false; 
        }

        // Cập nhật các thông tin khác của người dùng
        user.Dob = request.Dob;
        user.FullName = request.FullName;
        user.PhoneNumber = request.PhoneNumber;
        user.Email = request.Email;
        user.Gender = request.Gender;
        user.CCCD = request.CCCD;
        user.RestaurantID = request.RestaurantId;

        // Cập nhật thông tin người dùng
        var updateResult = await _userManager.UpdateAsync(user);
        if (!updateResult.Succeeded)
        {
            foreach (var error in updateResult.Errors)
            {
                Console.WriteLine($"Error: {error.Description}");
            }
            return false;
        }

        // Cập nhật các vai trò (Roles)
        var currentRoles = await _userManager.GetRolesAsync(user);
        
        // Xóa tất cả các vai trò hiện tại của người dùng
        var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
        if (!removeResult.Succeeded)
        {
            foreach (var error in removeResult.Errors)
            {
                Console.WriteLine($"Error removing roles: {error.Description}");
            }
            return false;
        }

        // Thêm các vai trò mới từ request.Roles
        if (request.Role != null && request.Role.Any())
        {
            var addRolesResult = await _userManager.AddToRoleAsync(user, request.Role);
            if (!addRolesResult.Succeeded)
            {
                foreach (var error in addRolesResult.Errors)
                {
                    Console.WriteLine($"Error adding roles: {error.Description}");
                }
                return false;
            }
        }

        return true;
    }

    public async Task<ApiResult<string>> BanUser(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
        {
            return new ApiErrorResult<string>("Người dùng không tồn tại.");
        }

        if (user.Status == 0)
        {
            user.Status = 1;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return new ApiSuccessResult<string>("Người dùng đã bị cấm thành công.");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"Error: {error.Description}");
                }
                return new ApiErrorResult<string>("Không thể cập nhật trạng thái người dùng.");
            }
        }
        else
        {
            user.Status = 0;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return new ApiSuccessResult<string>("Người dùng đã mở cấm thành công.");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"Error: {error.Description}");
                }
                return new ApiErrorResult<string>("Không thể cập nhật trạng thái người dùng.");
            }
        }

    }

    public async Task<ApiResult<List<UserRequestDto>>> GetAllCustomer()
    {
        // Lấy tất cả người dùng từ UserManager
        var users = await _userManager.Users.ToListAsync();

        // Tạo danh sách để lưu trữ kết quả trả về
        var customerUsers = new List<UserRequestDto>();

        // Lặp qua tất cả người dùng và lấy thông tin vai trò
        foreach (var user in users)
        {
            // Lấy các vai trò của người dùng
            var roles = await _userManager.GetRolesAsync(user);

            // Chỉ thêm những người dùng có role là Receptionist, Waiter, hoặc Manager
            if (roles.Any(role => role == "Customer"))
            {
                // Tạo đối tượng UserRequestDto bao gồm thông tin chi tiết và vai trò
                var customerUser = new UserRequestDto
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    UserName = user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Dob = user.Dob,
                    Gender = user.Gender,
                    CCCD = user.CCCD,
                    RestaurantID = user.RestaurantID,
                    Status=user.Status,
                    Roles = roles // Bao gồm thông tin về các vai trò
                };

                customerUsers.Add(customerUser);
            }
        }

        // Trả về danh sách nhân viên bao gồm các vai trò phù hợp
        return new ApiSuccessResult<List<UserRequestDto>>(customerUsers);
    }

    public async Task<ApiResult<bool>> ChangePassword(string email, ChangePasswordDto request)
    {
        var user=await _userManager.FindByEmailAsync(email);
        if(user==null) 
        {
            return new ApiErrorResult<bool>("Người dùng không tồn tại");
        }

        var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
        if(!result.Succeeded)
        {
            var errors=string.Join(", ", result.Errors.Select(e => e.Description));
            return new ApiErrorResult<bool>($"Đổi mật khẩu thất bại: {errors}");
        }
        return new ApiSuccessResult<bool>(true);
    }

    public async Task<ApiResult<EmailChecked>> SendMailCheckUser(string email, string? uri = null)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Email == email);
            if (user == null)
            {
                return new ApiErrorResult<EmailChecked>("Không có User");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            user.RecoveryToken = token; 
            await _userManager.UpdateAsync(user); 

            var param = new Dictionary<string, string?>
    {
        { "token", Uri.EscapeDataString(token) }, // Mã hóa token
        { "email", Uri.EscapeDataString(email) },
        { "userId", Uri.EscapeDataString(user.Id.ToString()) }
    };

            var callback = !string.IsNullOrEmpty(uri)
                ? QueryHelpers.AddQueryString(uri, param)
                : "http://localhost:3000/resetPassword" + QueryHelpers.AddQueryString("", param);

            return new ApiSuccessResult<EmailChecked>(new EmailChecked
            {
                ClientUrl = callback,
                Email = email,
                TokenRenew = token,
                UserId = user.Id.ToString()
            });
        }

    public async Task<ApiResult<bool>> CheckUserExists(string userName, string email)
        {
            var userByUserName = await _userManager.FindByNameAsync(userName);
            if (userByUserName != null)
            {
                return new ApiErrorResult<bool>("Username already exists.");
            }

            var userByEmail = await _userManager.FindByEmailAsync(email);
            if (userByEmail != null)
            {
                return new ApiErrorResult<bool>("Email already exists.");
            }

            return new ApiSuccessResult<bool>(true);
        }

    public async Task<ApiResult<bool>> RenewPassword(RenewPassword obj)
    {
        Console.WriteLine($"Received Token: {obj.TokenRenew}");
        var user = _userManager.Users.FirstOrDefault(u => u.RecoveryToken == obj.TokenRenew);

        if (user == null)
        {
            return new ApiErrorResult<bool>("Token không hợp lệ.");
        }

        // Đặt lại mật khẩu
        var result = await _userManager.ResetPasswordAsync(user, obj.TokenRenew, obj.PassWord);

        if (!result.Succeeded)
        {
            var errorMessages = result.Errors.Select(e => e.Description).ToList();
            return new ApiErrorResult<bool>(string.Join(", ", errorMessages));
        }

        // Xóa token sau khi đặt lại mật khẩu thành công (nếu cần)
        user.RecoveryToken = null;
        await _userManager.UpdateAsync(user);

        return new ApiSuccessResult<bool>(true);
    }

    public async Task<ApiResult<Tokens>> RenewToken(TokenRequestDto request)
    {
        var jwtTokenSecurityHandler = new JwtSecurityTokenHandler();
        var decodeToken = jwtTokenSecurityHandler.ReadJwtToken(request.Access_Token);
        
        var userName = decodeToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
        if (userName == null)
        {
            return new ApiErrorResult<Tokens>("Invalid token");
        }

        var user = await _userManager.FindByNameAsync(userName);
        if (user == null)
        {
            return new ApiErrorResult<Tokens>("User not found");
        }

        // Lấy các roles của người dùng từ UserManager
        var roles = await _userManager.GetRolesAsync(user);
        
        // Tạo các claims từ thông tin người dùng và các roles
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.FullName),
            new Claim(ClaimTypes.NameIdentifier, user.UserName)
        };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        // Tạo khóa bí mật và cấu hình mã ký
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // Tạo lại JWT với các claims
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Issuer"],
            claims: claims,
            expires: DateTime.Now.AddHours(3),
            signingCredentials: creds
        );

        string refreshToken = GenerateRefreshToken();

        // Cập nhật refresh token vào user
        user.RefreshToken = refreshToken;
        await _userManager.UpdateAsync(user);

        var getToken = new Tokens()
        {
            Access_Token = new JwtSecurityTokenHandler().WriteToken(token),
            Refresh_Token = refreshToken
        };

        return new ApiSuccessResult<Tokens>(getToken);
    }
    private string GenerateRefreshToken()
    {
        var random = new Byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(random);
            return Convert.ToBase64String(random);
        };
    }
}
