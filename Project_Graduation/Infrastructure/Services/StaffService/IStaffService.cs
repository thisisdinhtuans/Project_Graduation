using System;
using Domain.Models.Dto.Staff;

namespace Infrastructure.Services.StaffService;

public interface IStaffService
{
    public Task<bool> Register(StaffCreateDto request);
}
