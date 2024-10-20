using System;
using System.Security.Claims;
using Domain.Models.Dto.Role;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services.RoleService;

public class RoleService : IRoleService
{
    private readonly RoleManager<AppRole> _roleManager;

    public RoleService(RoleManager<AppRole> roleManager)
    {
        _roleManager = roleManager;
    }
    public async Task<bool> AddRoleClaimAsync(string roleName, Claim claim)
    {
        var role=await _roleManager.FindByNameAsync(roleName);
        if(role==null)
        {
            return false;
        }

        var result=await _roleManager.AddClaimAsync(role, claim);
        return result.Succeeded;
    }

    public async Task<bool> Edit(RoleRequestDto request)
    {
        var val = await _roleManager.FindByIdAsync(request.Id.ToString());
            if (val.Name == null)
            {
                return false;
            }
            val.Description = request.Description;
            val.Name = request.Name;
            var end = await _roleManager.UpdateAsync(val);
            if (end.Succeeded)
            {
                return true;
            }

            return false;
    }

    // public Task<List<RoleClaimRequestDto>> GetAllRoles()
    // {
    //     throw new NotImplementedException();
    // }

    public async Task<RoleRequestDto> GetById(string id)
    {
        var val = await _roleManager.FindByIdAsync(id);
            if (val.Name != null)
            {
                var role = new RoleRequestDto()
                {
                    Description = val.Description,
                    Name = val.Name,
                    Id = val.Id
                };
                return role;
            }
            throw new NotImplementedException();
    }

    public async Task<bool> Create(RoleRequestDto request)
    {
        var name = _roleManager.FindByNameAsync(request.Name);
            if (name.Result != null)
            {
                return false;
            }
            var user = new AppRole()
            {
                Description = request.Name,
                Name = request.Name,
                NormalizedName = request.Name
            };
            var end = await _roleManager.CreateAsync(user);
            if (end.Succeeded)
            {
                return true;
            }

            return false;
    }

    public async Task<bool> Remove(string id)
    {
        var val = await _roleManager.FindByIdAsync(id);
            if (val == null)
            {
                return false;
            }
            var end = await _roleManager.DeleteAsync(val);
            if (end.Succeeded)
            {
                return true;
            }

            return false;
    }
}
