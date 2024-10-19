using System;
using Domain.Models.Common.ApiResult;
using Domain.Models.Dto.Dish;
using Infrastructure.Entities;

namespace Infrastructure.Services.DishService;

public interface IDishService
{
    Task<ApiResult<List<DishDto>>> GetAllDishsAsync();
    Task<ApiResult<Dish>> GetDishByIdAsync(int id);
    Task<ApiResult<bool>> CreateDishAsync(CreateDishDto restaurantDto);
    Task<ApiResult<bool>> UpdateDishAsync(DishDto restaurantDto);
    Task<ApiResult<bool>> DeleteDishAsync(int id);
}