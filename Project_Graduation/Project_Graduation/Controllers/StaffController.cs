using System;
using Domain.Models.Dto.Staff;
using Infrastructure.Services.StaffService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Project_Graduation.Controllers;

public class StaffController:BaseApiController
{
        private readonly IStaffService _staffService;

        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }
        [Authorize(Roles = "Admin,Manager")]
        [HttpGet("get-full")]
        public async Task<IActionResult> GetAllStaff()
        {
            var staff=await _staffService.GetAllStaff();
            return Ok(staff);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] StaffCreateDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _staffService.Register(request);
            if (result == false)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
}
