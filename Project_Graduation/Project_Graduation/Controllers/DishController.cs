using System;
using AutoMapper;
using Domain.Models.Dto.Dish;
using Infrastructure.Entities;
using Infrastructure.Services.DishService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Project_Graduation.Controllers;
public class DishController : BaseApiController
{
    private readonly IDishService _dishService;
    private readonly IMapper _mapper;

    public DishController(IDishService dishService, IMapper mapper)
    {
        _dishService = dishService;
        _mapper = mapper;
    }

    // [HttpGet]
    // public async Task<ActionResult<PagedList<Dish>>> GetDishs([FromQuery] DishParams dishParams)
    // {
    //     var dishs = await _dishService.GetDishsAsync(dishParams);
    //     Response.AddPaginationHeader(dishs.MetaData);
    //     return Ok(dishs);
    // }

    [HttpGet("get-full")]
    public async Task<IActionResult> GetAllDishs()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        var dishs = await _dishService.GetAllDishsAsync();
        return Ok(dishs); // Tr? v? danh sách các nhà hàng
    }

    [HttpGet("get-by-id")]
    public async Task<ActionResult<Dish>> GetDish(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        var dish = await _dishService.GetDishByIdAsync(id);
        if (dish == null) return NotFound();
        return Ok(dish);
    }


    [Authorize(Roles = "Admin,Manager")]
    [HttpPost("add")]
    public async Task<IActionResult> CreateDish([FromBody] CreateDishDto dishDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        var result=await _dishService.CreateDishAsync(dishDto);
        //if (!result.IsSuccessed) return BadRequest(new ProblemDetails { Title = "Vấn đề khi thêm nhà hàng" });
        //return NoContent();
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("update")]
    public async Task<ActionResult> UpdateDish([FromBody] DishDto dishDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        var result = await _dishService.UpdateDishAsync(dishDto);
        //if (!result.IsSuccessed) return BadRequest(new ProblemDetails { Title = "Vấn đề khi cập nhật nhà hàng" });
        //return NoContent();
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("delete")]
    public async Task<ActionResult> DeleteDish([FromQuery]int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _dishService.DeleteDishAsync(id);
        //if (!result.IsSuccessed) return BadRequest(new ProblemDetails { Title = "Vấn đề khi xóa nhà hàng" });
        //return NoContent();
        return Ok(result);
    }
}