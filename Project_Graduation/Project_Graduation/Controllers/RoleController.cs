using System;
using System.Security.Claims;
using Domain.Models.Dto.Role;
using Infrastructure.Services.RoleService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Project_Graduation.Controllers;

[Authorize(Roles = "Admin")]
public class RoleController : BaseApiController
{
        private readonly IRoleService _serviceRole;

        public RoleController(IRoleService roleService)
        {
            _serviceRole = roleService;
        }

        // [HttpGet("getroles")]
        // public async Task<IActionResult> GetAll(int? pageSize, int? pageIndex, string? search)
        // {
        //     var roles = await _serviceRole.GetAllRole(pageSize, pageIndex, search);
        //     return Ok(roles);
        // }

        [HttpPost("add-role")]
        public async Task<IActionResult> CreaterRole(RoleRequestDto request)
        {
            var result = await _serviceRole.Create(request);
            return Ok();
        }

        [HttpPut("edit-role")]
        public async Task<IActionResult> EditRole(RoleRequestDto request)
        {
            var result = await _serviceRole.Edit(request);
            return Ok();
        }
        [HttpPost("add-role-claim")]
        public async Task<IActionResult> AddRoleClaim([FromBody] RoleClaimRequestDto request)
        {
            var result = await _serviceRole.AddRoleClaimAsync(request.RoleName, new Claim(request.ClaimType, request.ClaimValue));
            if (result)
            {
                return Ok(new { Success = true, Message = "Claim added to role successfully." });
            }
            return BadRequest(new { Success = false, Message = "Failed to add claim to role." });
        }
        [HttpDelete("delete-role")]
        public async Task<IActionResult> Remove(string id)
        {
            var result = await _serviceRole.Remove(id);
            return Ok();
        }

        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _serviceRole.GetById(id);
            return Ok(result);
        }
    }