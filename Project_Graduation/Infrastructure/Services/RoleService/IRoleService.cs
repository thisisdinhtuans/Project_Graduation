using System;
using System.Security.Claims;
using Domain.Models.Dto.Role;

namespace Infrastructure.Services.RoleService;

public interface IRoleService
{
    // public Task<List<RoleClaimRequestDto>> GetAllRoles();
    public Task<bool> Create(RoleRequestDto request);
    public Task<bool> AddRoleClaimAsync(string roleName, Claim claim);
    public Task<bool> Edit(RoleRequestDto request);

    public Task<bool> Remove(string id);

    public Task<RoleRequestDto> GetById(string id);
}