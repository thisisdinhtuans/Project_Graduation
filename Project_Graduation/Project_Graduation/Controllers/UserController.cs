using System;
using System.Security.Claims;
using Domain.Models.Dto.Login;
using Domain.Models.Dto.Role;
using Domain.Models.Dto.User;
using Infrastructure.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Project_Graduation.Lip;

namespace Project_Graduation.Controllers;

public class UserController:BaseApiController {
    private readonly IUserService _userService;
    private readonly IEmailSender _emailSender;

    public UserController(IUserService userService, IEmailSender emailSender)
    {
        _userService = userService;
        _emailSender = emailSender;
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
    [Authorize(Roles = "Admin,Manager,Owner,Waiter, Receptionist,Customer")]
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

    [Authorize]
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto request)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest("Dữ liệu không hợp lệ.");
        }

        var email=User.FindFirstValue(ClaimTypes.Email);
        if(string.IsNullOrEmpty(email))
        {
            return Unauthorized("Không xác định được người dùng");
        }

        var result=await _userService.ChangePassword(email,request);

        if(!result.IsSuccessed)
        {
            return BadRequest(result.Message);
        }
        return Ok("Đổi mật khẩu thành công");
    }
    [HttpGet("forgot")]
    [AllowAnonymous]
    public async Task<IActionResult> SendMailCheckUser(string email, string? uri = null)
    {
        var resultToken = await _userService.SendMailCheckUser(email, uri);
        if (!resultToken.IsSuccessed)
        {
            return BadRequest("Có lỗi xảy ra");
        }
        var message = new Message(new string[] { email }, "ForgotPassword", resultToken.ResultObj.ClientUrl);

        await _emailSender.SendEmailAsync(message);
        return Ok(resultToken);
    }
    [AllowAnonymous]
    [HttpGet("check-user-exists")]
    
    public async Task<IActionResult> CheckUserExists([FromQuery] string userName, [FromQuery] string email)
    {
        if (string.IsNullOrEmpty(userName) && string.IsNullOrEmpty(email))
        {
            return BadRequest("Username or email must be provided.");
        }

        var result = await _userService.CheckUserExists(userName, email);

        if (!result.IsSuccessed)
        {
            return Ok(new { Success = false, Message = result.Message });
        }

        return Ok(new { Success = true, Message = "Username and email are available." });
    }
    [HttpPost("renew-password")]
    [AllowAnonymous]
    public async Task<IActionResult> ReNewPassword([FromBody] RenewPassword obj)
    {
        var resultToken = await _userService.RenewPassword(obj);
        if (!resultToken.IsSuccessed)
        {
            return BadRequest(resultToken.Message);
        }
        return Ok(resultToken.IsSuccessed);
    }

    [Authorize]
    [HttpPost("renewToken")]
    public async Task<IActionResult> RenewToken(TokenRequestDto request)
    {
        if (ModelState.IsValid)
        {
            var resultToken = await _userService.RenewToken(request);
            if (resultToken.IsSuccessed)
            {
                return Ok(new LoginResponDto
                {
                    Time = 3,
                    Token = resultToken.ResultObj.Access_Token,
                    RefreshToken = resultToken.ResultObj.Refresh_Token,
                    Status = true
                });
            }
        }
        return BadRequest();
    }
}
