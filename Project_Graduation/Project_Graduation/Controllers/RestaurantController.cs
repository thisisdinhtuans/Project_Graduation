using System;
using Domain.Models.Common.ApiResult;
using Domain.Models.Dto.Restaurant;
using Infrastructure.Entities;
using Infrastructure.Services.RestaurantService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Project_Graduation.Controllers;

public class RestaurantsController : BaseApiController
{
    private readonly IRestaurantService _restaurantService;

    public RestaurantsController(IRestaurantService restaurantService)
    {
        _restaurantService = restaurantService;
    }

    // [HttpGet]
    // public async Task<ActionResult<PagedList<Restaurant>>> GetRestaurants([FromQuery] RestaurantParams restaurantParams)
    // {
    //     var restaurants = await _restaurantService.GetRestaurantsAsync(restaurantParams);
    //     Response.AddPaginationHeader(restaurants.MetaData);
    //     return Ok(restaurants);
    // }

    [HttpGet]
    public async Task<IActionResult> GetAllRestaurants()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        var restaurants = await _restaurantService.GetAllRestaurantsAsync();
        return Ok(restaurants); // Tr? v? danh s�ch c�c nh� h�ng
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
    [HttpPost]
    public async Task<ActionResult<Restaurant>> CreateRestaurant([FromBody] CreateRestaurantDto restaurantDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        var restaurant = await _restaurantService.CreateRestaurantAsync(restaurantDto);
        return Ok(restaurant);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut]
    public async Task<ActionResult<Restaurant>> UpdateRestaurant([FromBody] RestaurantDto restaurantDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        var restaurant = await _restaurantService.UpdateRestaurantAsync(restaurantDto);
        return Ok(restaurant);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteRestaurant(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _restaurantService.DeleteRestaurantAsync(id);
        if (!result.IsSuccessed) return BadRequest(new ProblemDetails { Title = "Problem deleting restaurant" });
        return NoContent();
    }
}