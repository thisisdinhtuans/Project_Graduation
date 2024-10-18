using System;
using Domain.Models.Common.ApiResult;
using Domain.Models.Dto.Area;
using Infrastructure.Entities;

namespace Infrastructure.Services.AreaService;

public interface IAreaService
{
    Task<ApiResult<List<AreaDto>>> GetAllAreasAsync();
    Task<ApiResult<Area>> GetAreaByIdAsync(int id);
    Task<ApiResult<bool>> CreateAreaAsync(CreateAreaDto restaurantDto);
    Task<ApiResult<bool>> UpdateAreaAsync(AreaDto restaurantDto);
    Task<ApiResult<bool>> DeleteAreaAsync(int id);
}