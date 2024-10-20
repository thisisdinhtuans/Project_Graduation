using System;
using Domain.Models.Dto.Role;
using Domain.Models.Dto.User;

namespace Infrastructure.Services.UserService;

public interface IUserService
{
    public Task<bool> Update(Guid id, UserUpdateRequestDto request);
    public Task<bool> Delete(Guid id);
    public Task<bool> RoleAssign(Guid id, RoleAssignRequestDto request);
    public Task<UserRequestDto> GetById(Guid id);
}
