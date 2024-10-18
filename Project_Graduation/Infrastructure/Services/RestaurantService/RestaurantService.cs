using System;
using AutoMapper;
using Domain.Models.Common;
using Domain.Models.Common.ApiResult;
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
    public async Task<ApiResult<bool>> CreateRestaurantAsync(CreateRestaurantDto restaurantDto)
    {
        if (restaurantDto == null)
        {
            throw new ArgumentNullException(nameof(restaurantDto));
        }

        var addressExists = await _restaurantRepository.AnyAsync(x => x.Address == restaurantDto.Address);
        if (addressExists)
        {
            return new ApiErrorResult<bool>("Nhà hàng với địa chỉ này đã tồn tại.");
        }


        var restaurant = _mapper.Map<Restaurant>(restaurantDto);
        // restaurant.CreatedBy=_httpContextAccessor.HttpContext.User.Identity.Name;
        // restaurant.CreatedDate=DateTime.Now;
        try
        {
            await _restaurantRepository.Add(restaurant);
            return new ApiSuccessResult<bool>(true);
        }
        catch (Exception ex)
        {
            return new ApiErrorResult<bool>(ex.Message);
        }
    }

    public async Task<ApiResult<bool>> UpdateRestaurantAsync(RestaurantDto restaurantDto)
    {
        var addressExists = await _restaurantRepository.AnyAsync(x => x.Address == restaurantDto.Address);
            if (addressExists)
            {
                return new ApiErrorResult<bool>("Nhà hàng với địa chỉ này đã tồn tại.");
            }

        var restaurant = await _restaurantRepository.GetByIdAsync(restaurantDto.RestaurantID);
        if (restaurant == null) throw new Exception("Restaurant not found");

        _mapper.Map(restaurantDto, restaurant);
        // restaurant.UpdatedDate = DateTime.Now;
        // restaurant.UpdatedBy = _httpContextAccessor.HttpContext.User.Identity.Name;
        await _restaurantRepository.Update(restaurant);
        try
        {
            await _restaurantRepository.Update(restaurant);
            return new ApiSuccessResult<bool>(true);
        }
        catch (Exception ex)
        {
            return new ApiErrorResult<bool>(ex.Message);
        }
    }

    public async Task<ApiResult<bool>> DeleteRestaurantAsync(int id)
    {
        var restaurant = await _restaurantRepository.GetByIdAsync(id);
        if (restaurant == null)
        {
            return new ApiErrorResult<bool>("Nhà hàng không được tìm thấy.");
        }

        try
        {
            await _restaurantRepository.Delete(restaurant);
            return new ApiSuccessResult<bool>(true);
        }
        catch (Exception ex)
        {
            return new ApiErrorResult<bool>(ex.Message);
        }
    }


    public async Task<ApiResult<Restaurant>> GetRestaurantByIdAsync(int id)
    {
        var restaurant= await _restaurantRepository.GetByIdAsync(id);
        return new ApiSuccessResult<Restaurant>(restaurant);
    }

    public async Task<ApiResult<List<RestaurantDto>>> GetAllRestaurantsAsync()
    {
        var restaurants = await _restaurantRepository.GetAllAsync(); // Lấy tất cả nhà hàng từ Repository
        var restaurantsDto=_mapper.Map<List<RestaurantDto>>(restaurants); // Chuyển đổi sang DTO
        return new ApiSuccessResult<List<RestaurantDto>>(restaurantsDto);
    }
}
