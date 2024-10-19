using System;
using AutoMapper;
using Domain.Models.Common;
using Domain.Models.Common.ApiResult;
using Domain.Models.Dto.Blog;
using Infrastructure.Entities;
using Infrastructure.Repositories.BlogRepository;

namespace Infrastructure.Services.BlogService;

public class BlogService: IBlogService
{
    private readonly IBlogRepository _blogRepository;
    private readonly IMapper _mapper;

    public BlogService(IBlogRepository blogRepository,IMapper mapper)
    {
        _blogRepository = blogRepository;
        _mapper = mapper;
    }
    public async Task<ApiResult<bool>> CreateBlogAsync(CreateBlogDto blogDto)
    {
        if (blogDto == null)
        {
            throw new ArgumentNullException(nameof(blogDto));
        }

        var addressExists = await _blogRepository.AnyAsync(x => x.Title == blogDto.Title);
        if (addressExists)
        {
            return new ApiErrorResult<bool>("Blog với tiêu đề này đã tồn tại.");
        }


        var blog = _mapper.Map<Blog>(blogDto);
        // blog.CreatedBy=_httpContextAccessor.HttpContext.User.Identity.Name;
        // blog.CreatedDate=DateTime.Now;
        try
        {
            await _blogRepository.Add(blog);
            return new ApiSuccessResult<bool>(true);
        }
        catch (Exception ex)
        {
            return new ApiErrorResult<bool>(ex.Message);
        }
    }

    public async Task<ApiResult<bool>> UpdateBlogAsync(BlogDto blogDto)
    {
        var addressExists = await _blogRepository.AnyAsync(x => x.Title == blogDto.Title);
            if (addressExists)
            {
                return new ApiErrorResult<bool>("Blog với tiêu đề này đã tồn tại.");
            }

        var blog = await _blogRepository.GetByIdAsync(blogDto.BlogID);
        if (blog == null) throw new Exception("Blog not found");

        _mapper.Map(blogDto, blog);
        // blog.UpdatedDate = DateTime.Now;
        // blog.UpdatedBy = _httpContextAccessor.HttpContext.User.Identity.Name;
        await _blogRepository.Update(blog);
        try
        {
            await _blogRepository.Update(blog);
            return new ApiSuccessResult<bool>(true);
        }
        catch (Exception ex)
        {
            return new ApiErrorResult<bool>(ex.Message);
        }
    }

    public async Task<ApiResult<bool>> DeleteBlogAsync(int id)
    {
        var blog = await _blogRepository.GetByIdAsync(id);
        if (blog == null)
        {
            return new ApiErrorResult<bool>("Blog không được tìm thấy.");
        }

        try
        {
            await _blogRepository.Delete(blog);
            return new ApiSuccessResult<bool>(true);
        }
        catch (Exception ex)
        {
            return new ApiErrorResult<bool>(ex.Message);
        }
    }


    public async Task<ApiResult<Blog>> GetBlogByIdAsync(int id)
    {
        var blog= await _blogRepository.GetByIdAsync(id);
        return new ApiSuccessResult<Blog>(blog);
    }

    public async Task<ApiResult<List<BlogDto>>> GetAllBlogsAsync()
    {
        var blogs = await _blogRepository.GetAllAsync(); // Lấy tất cả nhà hàng từ Repository
        var blogsDto=_mapper.Map<List<BlogDto>>(blogs); // Chuyển đổi sang DTO
        return new ApiSuccessResult<List<BlogDto>>(blogsDto);
    }
}