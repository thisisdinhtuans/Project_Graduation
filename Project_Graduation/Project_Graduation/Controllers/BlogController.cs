using System;
using AutoMapper;
using Domain.Models.Dto.Blog;
using Infrastructure.Entities;
using Infrastructure.Services.BlogService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Project_Graduation.Controllers;

public class BlogsController : BaseApiController
{
    private readonly IBlogService _blogService;
    private readonly IMapper _mapper;

    public BlogsController(IBlogService blogService, IMapper mapper)
    {
        _blogService = blogService;
        _mapper = mapper;
    }

    // [HttpGet]
    // public async Task<ActionResult<PagedList<Blog>>> GetBlogs([FromQuery] BlogParams blogParams)
    // {
    //     var blogs = await _blogService.GetBlogsAsync(blogParams);
    //     Response.AddPaginationHeader(blogs.MetaData);
    //     return Ok(blogs);
    // }

    [HttpGet("get-full")]
    public async Task<IActionResult> GetAllBlogs()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        var blogs = await _blogService.GetAllBlogsAsync();
        return Ok(blogs); // Tr? v? danh sách các nhà hàng
    }

    [HttpGet("get-by-id")]
    public async Task<ActionResult<Blog>> GetBlog(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        var blog = await _blogService.GetBlogByIdAsync(id);
        if (blog == null) return NotFound();
        return Ok(blog);
    }


    [Authorize(Roles = "Admin,Manager")]
    [HttpPost("add")]
    public async Task<IActionResult> CreateBlog([FromBody] CreateBlogDto blogDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        var result = await _blogService.CreateBlogAsync(blogDto);
        //if (!result.IsSuccessed) return BadRequest(new ProblemDetails { Title = "Vấn đề khi thêm nhà hàng" });
        //return NoContent();
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("update")]
    public async Task<ActionResult> UpdateBlog([FromBody] BlogDto blogDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        var result = await _blogService.UpdateBlogAsync(blogDto);
        // if (!result.IsSuccessed) return BadRequest(new ProblemDetails { Title = "Vấn đề khi cập nhật nhà hàng" });
        // return NoContent();
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("delete")]
    public async Task<ActionResult> DeleteBlog([FromQuery]int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _blogService.DeleteBlogAsync(id);
        //if (!result.IsSuccessed) return BadRequest(new ProblemDetails { Title = "Vấn đề khi xóa nhà hàng" });
        //return NoContent();
        return Ok(result);
    }
}