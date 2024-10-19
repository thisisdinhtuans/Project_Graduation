using System;
using AutoMapper;
using Domain.Models.Common;
using Domain.Models.Common.ApiResult;
using Domain.Models.Dto.Dish;
using Infrastructure.Entities;
using Infrastructure.Repositories.CategoryRepository;
using Infrastructure.Repositories.DishRepository;

namespace Infrastructure.Services.DishService;

public class DishService: IDishService
{
    private readonly IDishRepository _dishRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public DishService(IDishRepository dishRepository,ICategoryRepository categoryRepository, IMapper mapper)
    {
        _dishRepository = dishRepository;
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }
    public async Task<ApiResult<bool>> CreateDishAsync(CreateDishDto dishDto)
    {
        var category =await _categoryRepository.GetByIdAsync(dishDto.CategoryID);
        if(category==null)
        {
            return new ApiErrorResult<bool>("Loại món này không tồn tại");
        }
        if (dishDto == null)
        {
            throw new ArgumentNullException(nameof(dishDto));
        }

        var addressExists = await _dishRepository.AnyAsync(x => x.Name == dishDto.Name);
        if (addressExists)
        {
            return new ApiErrorResult<bool>("Món  này đã tồn tại.");
        }


        var dish = _mapper.Map<Dish>(dishDto);
        // dish.CreatedBy=_httpContextAccessor.HttpContext.User.Identity.Name;
        // dish.CreatedDate=DateTime.Now;
        try
        {
            await _dishRepository.Add(dish);
            return new ApiSuccessResult<bool>(true);
        }
        catch (Exception ex)
        {
            return new ApiErrorResult<bool>(ex.Message);
        }

    }

    public async Task<ApiResult<bool>> UpdateDishAsync(DishDto dishDto)
    {
        var addressExists = await _dishRepository.AnyAsync(x => x.Name == dishDto.Name);
            if (addressExists)
            {
                return new ApiErrorResult<bool>("Món  này đã tồn tại.");
            }

        var dish = await _dishRepository.GetByIdAsync(dishDto.DishId);
        if (dish == null) throw new Exception("Dish not found");

        _mapper.Map(dishDto, dish);
        // dish.UpdatedDate = DateTime.Now;
        // dish.UpdatedBy = _httpContextAccessor.HttpContext.User.Identity.Name;
        try
        {
            await _dishRepository.Update(dish);
            return new ApiSuccessResult<bool>(true);
        }
        catch (Exception ex)
        {
            return new ApiErrorResult<bool>(ex.Message);
        }
    }

    public async Task<ApiResult<bool>> DeleteDishAsync(int id)
    {
        var dish = await _dishRepository.GetByIdAsync(id);
        if (dish == null)
        {
            return new ApiErrorResult<bool>("Món không được tìm thấy.");
        }

        try
        {
            await _dishRepository.Delete(dish);
            return new ApiSuccessResult<bool>(true);
        }
        catch (Exception ex)
        {
            return new ApiErrorResult<bool>(ex.Message);
        }
    }


    public async Task<ApiResult<Dish>> GetDishByIdAsync(int id)
    {
        var dish= await _dishRepository.GetByIdAsync(id);
        return new ApiSuccessResult<Dish>(dish);
    }

    public async Task<ApiResult<List<DishDto>>> GetAllDishsAsync()
    {
        var dishs = await _dishRepository.GetAllAsync(); // Lấy tất cả nhà hàng từ Repository
        var dishsDto=_mapper.Map<List<DishDto>>(dishs); // Chuyển đổi sang DTO
        return new ApiSuccessResult<List<DishDto>>(dishsDto);
    }
}
