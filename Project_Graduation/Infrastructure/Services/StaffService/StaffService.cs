using System;
using Domain.Models.Common.ApiResult;
using Domain.Models.Dto.Staff;
using Domain.Models.Dto.User;
using Infrastructure.Data;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.StaffService;

public class StaffService : IStaffService
{
    private readonly Project_Graduation_Context _dbcontext;
    private readonly UserManager<AppUser> _userManager;

    public StaffService(Project_Graduation_Context dbcontext,UserManager<AppUser> userManager)
    {
        _dbcontext = dbcontext;
        _userManager = userManager;
    }

    public async Task<ApiResult<List<UserRequestDto>>> GetAllStaff()
    {
        // Lấy tất cả người dùng từ UserManager
        var users = await _userManager.Users.ToListAsync();

        // Tạo danh sách để lưu trữ kết quả trả về
        var staffUsers = new List<UserRequestDto>();

        // Lặp qua tất cả người dùng và lấy thông tin vai trò
        foreach (var user in users)
        {
            // Lấy các vai trò của người dùng
            var roles = await _userManager.GetRolesAsync(user);

            // Chỉ thêm những người dùng có role là Receptionist, Waiter, hoặc Manager
            if (roles.Any(role => role == "Receptionist" || role == "Waiter" || role == "Manager"))
            {
                // Tạo đối tượng UserRequestDto bao gồm thông tin chi tiết và vai trò
                var staffUser = new UserRequestDto
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

                staffUsers.Add(staffUser);
            }
        }

        // Trả về danh sách nhân viên bao gồm các vai trò phù hợp
        return new ApiSuccessResult<List<UserRequestDto>>(staffUsers);
    }



    public async Task<bool> Register(StaffCreateDto request)
    {
        var emailExist = _userManager.Users.Any(x => x.Email == request.Email);
            if (emailExist)
            {
                return false;
            }

            var userNameExist = await _userManager.FindByNameAsync(request.UserName);
            if (userNameExist != null)
            {
                return false;
            }

            var user = new AppUser()
            {
                Dob = request.Dob,
                Email = request.Email,
                FullName = request.FullName,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber,
                Gender = request.Gender,
                CCCD = request.CCCD,
                RestaurantID = request.RestaurantID
            };

            var result = await _userManager.CreateAsync(user, request.PassWord);
            if (result.Succeeded)
            {
                // Add user to role
                var roleResult = await _userManager.AddToRoleAsync(user, request.Role);
                if (roleResult.Succeeded)
                {
                    return true;
                }
            }
            return false;
    }
}
