using System;
using Domain.Models.Dto.Restaurant;
using Infrastructure.Entities;

namespace Infrastructure.Services.RestaurantService;

public interface IRestaurantService
{
    Task<List<RestaurantDto>> GetAllRestaurantsAsync();
    Task<Restaurant> GetRestaurantByIdAsync(int id);
    Task<Restaurant> CreateRestaurantAsync(CreateRestaurantDto restaurantDto);
    Task<Restaurant> UpdateRestaurantAsync(RestaurantDto restaurantDto);
    Task<bool> DeleteRestaurantAsync(int id);
}
