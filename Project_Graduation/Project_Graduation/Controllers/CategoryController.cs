using System;
using AutoMapper;
using Domain.Models.Dto.Category;
using Infrastructure.Entities;
using Infrastructure.Services.CategoryService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Project_Graduation.Controllers;

public class CategoriesController : BaseApiController
{
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;

    public CategoriesController(ICategoryService categoryService, IMapper mapper)
    {
        _categoryService = categoryService;
        _mapper = mapper;
    }

    // [HttpGet]
    // public async Task<ActionResult<PagedList<Category>>> GetCategories([FromQuery] CategoryParams categoryParams)
    // {
    //     var categorys = await _categoryService.GetCategoriesAsync(categoryParams);
    //     Response.AddPaginationHeader(categorys.MetaData);
    //     return Ok(categorys);
    // }

    [HttpGet("get-full")]
    public async Task<IActionResult> GetAllCategories()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        var categorys = await _categoryService.GetAllCategoriesAsync();
        return Ok(categorys); // Tr? v? danh sách các nhà hàng
    }

    [HttpGet("{id}", Name = "GetCategory")]
    public async Task<ActionResult<Category>> GetCategory(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        var category = await _categoryService.GetCategoryByIdAsync(id);
        if (category == null) return NotFound();
        return Ok(category);
    }


    [Authorize(Roles = "Admin,Manager")]
    [HttpPost("add")]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto categoryDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        var result = await _categoryService.CreateCategoryAsync(categoryDto);
        //if (!result.IsSuccessed) return BadRequest(new ProblemDetails { Title = "Vấn đề khi thêm nhà hàng" });
        //return NoContent();
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("update")]
    public async Task<ActionResult> UpdateCategory([FromBody] CategoryDto categoryDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        var result = await _categoryService.UpdateCategoryAsync(categoryDto);
        // if (!result.IsSuccessed) return BadRequest(new ProblemDetails { Title = "Vấn đề khi cập nhật nhà hàng" });
        // return NoContent();
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("delete")]
    public async Task<ActionResult> DeleteCategory([FromQuery] int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _categoryService.DeleteCategoryAsync(id);
        //if (!result.IsSuccessed) return BadRequest(new ProblemDetails { Title = "Vấn đề khi xóa nhà hàng" });
        //return NoContent();
        return Ok(result);
    }
}
