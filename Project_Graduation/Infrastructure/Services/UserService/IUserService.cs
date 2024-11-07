using System;
using Domain.Models.Common.ApiResult;
using Domain.Models.Dto.Login;
using Domain.Models.Dto.Role;
using Domain.Models.Dto.User;

namespace Infrastructure.Services.UserService;

public interface IUserService
{
    Task<ApiResult<bool>> ChangePassword(string email, ChangePasswordDto request);
    public Task<bool> Update(Guid id, UserUpdateRequestDto request);
    public Task<bool> Delete(Guid id);
    public Task<bool> RoleAssign(Guid id, RoleAssignRequestDto request);
    public Task<UserRequestDto> GetById(Guid id);
    public Task<ApiResult<string>> BanUser(Guid userId);
    Task<ApiResult<List<UserRequestDto>>> GetAllCustomer();
}
