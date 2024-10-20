using System;
using Domain.Models.Dto.Staff;
using Infrastructure.Data;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;

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
                RestaurantID = request.RestaurantID,
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
