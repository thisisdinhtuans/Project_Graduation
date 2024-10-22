using System;
using Domain.Models.Common.ApiResult;
using Domain.Models.Dto.Staff;
using Domain.Models.Dto.User;
using Infrastructure.Entities;

namespace Infrastructure.Services.StaffService;

public interface IStaffService
{
    Task<bool> Register(StaffCreateDto request);
    Task<ApiResult<List<UserRequestDto>>> GetAllStaff();
}
