using System;
using Domain.Features;
using Domain.Models.Dto.Login;
using Microsoft.AspNetCore.Mvc;

namespace Project_Graduation.Controllers;

public class AccountController : BaseApiController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginRequestDto loginDto)
        {
            var userDto = await _accountService.LoginAsync(loginDto);
            if (userDto == null)
            {
                return Unauthorized();
            }

            return Ok(userDto);
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDto registerDto)
        {
            var result = await _accountService.RegisterAsync(registerDto);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return ValidationProblem();
            }
            
            return StatusCode(201);
        }
    }
