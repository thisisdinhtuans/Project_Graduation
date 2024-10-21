using System;
using Domain.Models.Common.ApiResult;
using Domain.Models.Dto.Blog;
using Infrastructure.Entities;

namespace Infrastructure.Services.BlogService;

public interface IBlogService
{
    Task<ApiResult<List<Blog>>> GetAllBlogsAsync();
    Task<ApiResult<Blog>> GetBlogByIdAsync(int id);
    Task<ApiResult<bool>> CreateBlogAsync(CreateBlogDto blogDto);
    Task<ApiResult<bool>> UpdateBlogAsync(BlogDto blogDto);
    Task<ApiResult<bool>> DeleteBlogAsync(int id);
}
