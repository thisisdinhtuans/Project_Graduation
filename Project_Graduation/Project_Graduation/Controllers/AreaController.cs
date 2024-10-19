using System;
using AutoMapper;
using Domain.Models.Dto.Area;
using Infrastructure.Entities;
using Infrastructure.Services.AreaService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Project_Graduation.Controllers;

public class AreasController : BaseApiController
{
    private readonly IAreaService _areaService;
    private readonly IMapper _mapper;

    public AreasController(IAreaService areaService, IMapper mapper)
    {
        _areaService = areaService;
        _mapper = mapper;
    }

    // [HttpGet]
    // public async Task<ActionResult<PagedList<Area>>> GetAreas([FromQuery] AreaParams areaParams)
    // {
    //     var areas = await _areaService.GetAreasAsync(areaParams);
    //     Response.AddPaginationHeader(areas.MetaData);
    //     return Ok(areas);
    // }

    [HttpGet]
    public async Task<IActionResult> GetAllAreas()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        var areas = await _areaService.GetAllAreasAsync();
        return Ok(areas); // Tr? v? danh sách các nhà hàng
    }

    [HttpGet("{id}", Name = "GetArea")]
    public async Task<ActionResult<Area>> GetArea(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        var area = await _areaService.GetAreaByIdAsync(id);
        if (area == null) return NotFound();
        return Ok(area);
    }


    [Authorize(Roles = "Admin,Manager")]
    [HttpPost]
    public async Task<IActionResult> CreateArea([FromBody] CreateAreaDto areaDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        var result=await _areaService.CreateAreaAsync(areaDto);
        //if (!result.IsSuccessed) return BadRequest(new ProblemDetails { Title = "Vấn đề khi thêm nhà hàng" });
        //return NoContent();
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut]
    public async Task<ActionResult> UpdateArea([FromBody] AreaDto areaDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        var result = await _areaService.UpdateAreaAsync(areaDto);
        //if (!result.IsSuccessed) return BadRequest(new ProblemDetails { Title = "Vấn đề khi cập nhật nhà hàng" });
        //return NoContent();
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteArea(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _areaService.DeleteAreaAsync(id);
        //if (!result.IsSuccessed) return BadRequest(new ProblemDetails { Title = "Vấn đề khi xóa nhà hàng" });
        //return NoContent();
        return Ok(result);
    }
}