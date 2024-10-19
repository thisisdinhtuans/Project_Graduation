using System;
using AutoMapper;
using Domain.Models.Common;
using Domain.Models.Common.ApiResult;
using Domain.Models.Dto.Category;
using Infrastructure.Entities;
using Infrastructure.Repositories.CategoryRepository;

namespace Infrastructure.Services.CategoryService;

public class CategoryService: ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository categoryRepository,IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }
    public async Task<ApiResult<bool>> CreateCategoryAsync(CreateCategoryDto categoryDto)
    {
        if (categoryDto == null)
        {
            throw new ArgumentNullException(nameof(categoryDto));
        }

        var addressExists = await _categoryRepository.AnyAsync(x => x.Name == categoryDto.Name);
        if (addressExists)
        {
            return new ApiErrorResult<bool>("Loại món ăn với tên này đã tồn tại.");
        }


        var category = _mapper.Map<Category>(categoryDto);
        // category.CreatedBy=_httpContextAccessor.HttpContext.User.Identity.Name;
        // category.CreatedDate=DateTime.Now;
        try
        {
            await _categoryRepository.Add(category);
            return new ApiSuccessResult<bool>(true);
        }
        catch (Exception ex)
        {
            return new ApiErrorResult<bool>(ex.Message);
        }
    }

    public async Task<ApiResult<bool>> UpdateCategoryAsync(CategoryDto categoryDto)
    {
        var addressExists = await _categoryRepository.AnyAsync(x => x.Name == categoryDto.Name);
            if (addressExists)
            {
                return new ApiErrorResult<bool>("Loại món ăn với tên này đã tồn tại.");
            }

        var category = await _categoryRepository.GetByIdAsync(categoryDto.IdCategory);
        if (category == null) throw new Exception("Category not found");

        _mapper.Map(categoryDto, category);
        // category.UpdatedDate = DateTime.Now;
        // category.UpdatedBy = _httpContextAccessor.HttpContext.User.Identity.Name;
        await _categoryRepository.Update(category);
        try
        {
            await _categoryRepository.Update(category);
            return new ApiSuccessResult<bool>(true);
        }
        catch (Exception ex)
        {
            return new ApiErrorResult<bool>(ex.Message);
        }
    }

    public async Task<ApiResult<bool>> DeleteCategoryAsync(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null)
        {
            return new ApiErrorResult<bool>("Loại món ăn không được tìm thấy.");
        }

        try
        {
            await _categoryRepository.Delete(category);
            return new ApiSuccessResult<bool>(true);
        }
        catch (Exception ex)
        {
            return new ApiErrorResult<bool>(ex.Message);
        }
    }


    public async Task<ApiResult<Category>> GetCategoryByIdAsync(int id)
    {
        var category= await _categoryRepository.GetByIdAsync(id);
        return new ApiSuccessResult<Category>(category);
    }

    public async Task<ApiResult<List<CategoryDto>>> GetAllCategoriesAsync()
    {
        var categorys = await _categoryRepository.GetAllAsync(); // Lấy tất cả nhà hàng từ Repository
        var categorysDto=_mapper.Map<List<CategoryDto>>(categorys); // Chuyển đổi sang DTO
        return new ApiSuccessResult<List<CategoryDto>>(categorysDto);
    }
}

