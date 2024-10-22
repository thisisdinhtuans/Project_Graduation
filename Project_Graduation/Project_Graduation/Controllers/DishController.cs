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
    private readonly IDishService _areaService;
    private readonly IMapper _mapper;

    public DishController(IDishService areaService, IMapper mapper)
    {
        _areaService = areaService;
        _mapper = mapper;
    }

    // [HttpGet]
    // public async Task<ActionResult<PagedList<Dish>>> GetDishs([FromQuery] DishParams areaParams)
    // {
    //     var areas = await _areaService.GetDishsAsync(areaParams);
    //     Response.AddPaginationHeader(areas.MetaData);
    //     return Ok(areas);
    // }

    [HttpGet("get-full")]
    public async Task<IActionResult> GetAllDishs()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        var areas = await _areaService.GetAllDishsAsync();
        return Ok(areas); // Tr? v? danh sách các nhà hàng
    }

    [HttpGet("get-by-id")]
    public async Task<ActionResult<Dish>> GetDish(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        var area = await _areaService.GetDishByIdAsync(id);
        if (area == null) return NotFound();
        return Ok(area);
    }


    [Authorize(Roles = "Admin,Manager")]
    [HttpPost("add")]
    public async Task<IActionResult> CreateDish([FromBody] CreateDishDto areaDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        var result=await _areaService.CreateDishAsync(areaDto);
        //if (!result.IsSuccessed) return BadRequest(new ProblemDetails { Title = "Vấn đề khi thêm nhà hàng" });
        //return NoContent();
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut]
    public async Task<ActionResult> UpdateDish([FromBody] DishDto areaDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        var result = await _areaService.UpdateDishAsync(areaDto);
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

        var result = await _areaService.DeleteDishAsync(id);
        //if (!result.IsSuccessed) return BadRequest(new ProblemDetails { Title = "Vấn đề khi xóa nhà hàng" });
        //return NoContent();
        return Ok(result);
    }
}