using System;
using AutoMapper;
using Domain.Models.Dto.Restaurant;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services.RestaurantService;

public class RestaurantService: IRestaurantService
{
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IMapper _mapper;

    public RestaurantService(IRestaurantRepository restaurantRepository,IMapper mapper)
    {
        _restaurantRepository = restaurantRepository;
        _mapper = mapper;
    }
    public async Task<Restaurant> CreateRestaurantAsync(CreateRestaurantDto restaurantDto)
    {
        var restaurant = _mapper.Map<Restaurant>(restaurantDto);
        // restaurant.CreatedBy=_httpContextAccessor.HttpContext.User.Identity.Name;
        // restaurant.CreatedDate=DateTime.Now;
        _restaurantRepository.AddAsync(restaurant);
        
        return restaurant;
    }

    public async Task<Restaurant> UpdateRestaurantAsync(RestaurantDto restaurantDto)
    {
        var restaurant = await _restaurantRepository.GetByIdAsync(restaurantDto.RestaurantID);
        if (restaurant == null) throw new Exception("Restaurant not found");

        _mapper.Map(restaurantDto, restaurant);
        // restaurant.UpdatedDate = DateTime.Now;
        // restaurant.UpdatedBy = _httpContextAccessor.HttpContext.User.Identity.Name;
        _restaurantRepository.Update(restaurant);

        return restaurant;
    }

    public async Task<bool> DeleteRestaurantAsync(int id)
    {
        var restaurant = await _restaurantRepository.GetByIdAsync(id);
        if (restaurant == null) return false;

        return await _restaurantRepository.Delete(restaurant);
    }

    public async Task<Restaurant> GetRestaurantByIdAsync(int id)
    {
        return await _restaurantRepository.GetByIdAsync(id);
    }

    public async Task<List<RestaurantDto>> GetAllRestaurantsAsync()
    {
        var restaurants = await _restaurantRepository.GetAllAsync(); // Lấy tất cả nhà hàng từ Repository
        return _mapper.Map<List<RestaurantDto>>(restaurants); // Chuyển đổi sang DTO
    }
}
