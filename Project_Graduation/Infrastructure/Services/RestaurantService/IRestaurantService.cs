using System;
using Domain.Models.Common.ApiResult;
using Domain.Models.Dto.Restaurant;
using Infrastructure.Entities;

namespace Infrastructure.Services.RestaurantService;

public interface IRestaurantService
{
    Task<ApiResult<List<RestaurantDto>>> GetAllRestaurantsAsync();
    Task<ApiResult<Restaurant>> GetRestaurantByIdAsync(int id);
    Task<ApiResult<bool>> CreateRestaurantAsync(CreateRestaurantDto restaurantDto);
    Task<ApiResult<bool>> UpdateRestaurantAsync(RestaurantDto restaurantDto);
    Task<ApiResult<bool>> DeleteRestaurantAsync(int id);
}
