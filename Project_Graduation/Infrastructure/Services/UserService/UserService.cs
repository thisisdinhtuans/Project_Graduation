using System;
using Domain.Models.Common;
using Domain.Models.Common.ApiResult;
using Domain.Models.Dto.Role;
using Domain.Models.Dto.User;
using Infrastructure.Data;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services.UserService;

public class UserService : IUserService
{
        private readonly Project_Graduation_Context _dbcontext;
        private readonly UserManager<AppUser> _userManager;

        public UserService(Project_Graduation_Context dbcontext, UserManager<AppUser> userManager)
        {
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
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FullName = user.FullName,
                Id = id,
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
}
