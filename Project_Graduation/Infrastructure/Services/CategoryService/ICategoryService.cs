using System;
using Domain.Models.Common.ApiResult;
using Domain.Models.Dto.Category;
using Infrastructure.Entities;

namespace Infrastructure.Services.CategoryService;

public interface ICategoryService
{
    Task<ApiResult<List<CategoryDto>>> GetAllCategoriesAsync();
    Task<ApiResult<Category>> GetCategoryByIdAsync(int id);
    Task<ApiResult<bool>> CreateCategoryAsync(CreateCategoryDto categoryDto);
    Task<ApiResult<bool>> UpdateCategoryAsync(CategoryDto categoryDto);
    Task<ApiResult<bool>> DeleteCategoryAsync(int id);
}
