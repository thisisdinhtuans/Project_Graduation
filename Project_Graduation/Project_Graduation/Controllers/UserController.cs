using System;
using Domain.Models.Dto.Role;
using Domain.Models.Dto.User;
using Infrastructure.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Project_Graduation.Controllers;

public class UserController:BaseApiController {
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    [HttpPut("update")]
    [Authorize]
    public async Task<IActionResult> Update(Guid id, [FromBody] UserUpdateRequestDto request)
    {

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _userService.Update(id, request);
        if (result == false)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }
    [Authorize(Roles = "Admin")]
    [HttpDelete]
    public async Task<IActionResult> Delete([FromForm] Guid id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _userService.Delete(id);
        if (result == false)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}/roles")]
    public async Task<IActionResult> RoleAssign(Guid id, [FromBody] RoleAssignRequestDto request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _userService.RoleAssign(id, request);
        if (result == false)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }
    [Authorize(Roles = "Admin,Manager,Owner, Receptionist,Customer")]
    [HttpGet("get-by-id")]
    public async Task<IActionResult> GetById([FromQuery]Guid Id)
    {
        var products = await _userService.GetById(Id);
        return Ok(products);
    }
    [Authorize(Roles = "Admin")]
    [HttpPut("{id}/ban")]
    public async Task<IActionResult> BanUser(Guid id)
    {
        var result = await _userService.BanUser(id);

        if (result.IsSuccessed)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest(result);
        }
    }

    [Authorize(Roles = "Admin,Manager")]
    [HttpGet("get-full-customer")]
    public async Task<IActionResult> GetAllCustomer()
    {
        var customer=await _userService.GetAllCustomer();
        return Ok(customer);
    }
}
