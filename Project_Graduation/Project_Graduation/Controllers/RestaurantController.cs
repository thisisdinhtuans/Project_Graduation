using System;
using AutoMapper;
using Domain.Models.Dto.Restaurant;
using Infrastructure.Entities;
using Infrastructure.Services.RestaurantService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Project_Graduation.Controllers;

public class RestaurantsController : BaseApiController
{
    private readonly IRestaurantService _restaurantService;
    private readonly IMapper _mapper;

    public RestaurantsController(IRestaurantService restaurantService, IMapper mapper)
    {
        _restaurantService = restaurantService;
        _mapper = mapper;
    }

    // [HttpGet]
    // public async Task<ActionResult<PagedList<Restaurant>>> GetRestaurants([FromQuery] RestaurantParams restaurantParams)
    // {
    //     var restaurants = await _restaurantService.GetRestaurantsAsync(restaurantParams);
    //     Response.AddPaginationHeader(restaurants.MetaData);
    //     return Ok(restaurants);
    // }

    [HttpGet("get-full")]
    public async Task<IActionResult> GetAllRestaurants()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        var restaurants = await _restaurantService.GetAllRestaurantsAsync();
        return Ok(restaurants); // Tr? v? danh sách các nhà hàng
    }

    [HttpGet("{id}", Name = "GetRestaurant")]
    public async Task<ActionResult<Restaurant>> GetRestaurant(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        var restaurant = await _restaurantService.GetRestaurantByIdAsync(id);
        if (restaurant == null) return NotFound();
        return Ok(restaurant);
    }


    [Authorize(Roles = "Admin,Manager")]
    [HttpPost("add")]
    public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantDto restaurantDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        var result = await _restaurantService.CreateRestaurantAsync(restaurantDto);
        //if (!result.IsSuccessed) return BadRequest(new ProblemDetails { Title = "Vấn đề khi thêm nhà hàng" });
        //return NoContent();
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("update")]
    public async Task<ActionResult> UpdateRestaurant([FromBody] RestaurantDto restaurantDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        var result = await _restaurantService.UpdateRestaurantAsync(restaurantDto);
        // if (!result.IsSuccessed) return BadRequest(new ProblemDetails { Title = "Vấn đề khi cập nhật nhà hàng" });
        // return NoContent();
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("delete")]
    public async Task<ActionResult> DeleteRestaurant([FromQuery] int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _restaurantService.DeleteRestaurantAsync(id);
        //if (!result.IsSuccessed) return BadRequest(new ProblemDetails { Title = "Vấn đề khi xóa nhà hàng" });
        //return NoContent();
        return Ok(result);
    }
}