using System;
using AutoMapper;
using Domain.Models.Common;
using Domain.Models.Common.ApiResult;
using Domain.Models.Dto.Area;
using Infrastructure.Entities;
using Infrastructure.Repositories.AreaRepository;

namespace Infrastructure.Services.AreaService;

public class AreaService: IAreaService
{
    private readonly IAreaRepository _areaRepository;
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IMapper _mapper;

    public AreaService(IAreaRepository areaRepository,IRestaurantRepository restaurantRepository, IMapper mapper)
    {
        _areaRepository = areaRepository;
        _restaurantRepository = restaurantRepository;
        _mapper = mapper;
    }
    public async Task<ApiResult<bool>> CreateAreaAsync(CreateAreaDto areaDto)
    {
        var restaurant = _restaurantRepository.GetByIdAsync(areaDto.RestaurantID);
        if(restaurant==null)
        {
            return new ApiErrorResult<bool>("Nhà hàng này không tồn tại");
        }
        if (areaDto == null)
        {
            throw new ArgumentNullException(nameof(areaDto));
        }

        var addressExists = await _areaRepository.AnyAsync(x => x.AreaName == areaDto.AreaName);
        if (addressExists)
        {
            return new ApiErrorResult<bool>("Khu vực  này đã tồn tại.");
        }


        var area = _mapper.Map<Area>(areaDto);
        // area.CreatedBy=_httpContextAccessor.HttpContext.User.Identity.Name;
        // area.CreatedDate=DateTime.Now;
        try
        {
            await _areaRepository.Add(area);
            return new ApiSuccessResult<bool>(true);
        }
        catch (Exception ex)
        {
            return new ApiErrorResult<bool>(ex.Message);
        }

    }

    public async Task<ApiResult<bool>> UpdateAreaAsync(AreaDto areaDto)
    {
        var addressExists = await _areaRepository.AnyAsync(x => x.AreaName == areaDto.AreaName);
            if (addressExists)
            {
                return new ApiErrorResult<bool>("Khu vực  này đã tồn tại.");
            }

        var area = await _areaRepository.GetByIdAsync(areaDto.AreaID);
        if (area == null) throw new Exception("Area not found");

        _mapper.Map(areaDto, area);
        // area.UpdatedDate = DateTime.Now;
        // area.UpdatedBy = _httpContextAccessor.HttpContext.User.Identity.Name;
        try
        {
            await _areaRepository.Update(area);
            return new ApiSuccessResult<bool>(true);
        }
        catch (Exception ex)
        {
            return new ApiErrorResult<bool>(ex.Message);
        }
    }

    public async Task<ApiResult<bool>> DeleteAreaAsync(int id)
    {
        var area = await _areaRepository.GetByIdAsync(id);
        if (area == null)
        {
            return new ApiErrorResult<bool>("Khu vực không được tìm thấy.");
        }

        try
        {
            await _areaRepository.Delete(area);
            return new ApiSuccessResult<bool>(true);
        }
        catch (Exception ex)
        {
            return new ApiErrorResult<bool>(ex.Message);
        }
    }


    public async Task<ApiResult<Area>> GetAreaByIdAsync(int id)
    {
        var area= await _areaRepository.GetByIdAsync(id);
        return new ApiSuccessResult<Area>(area);
    }

    public async Task<ApiResult<List<AreaDto>>> GetAllAreasAsync()
    {
        var areas = await _areaRepository.GetAllAsync(); // Lấy tất cả nhà hàng từ Repository
        var areasDto=_mapper.Map<List<AreaDto>>(areas); // Chuyển đổi sang DTO
        return new ApiSuccessResult<List<AreaDto>>(areasDto);
    }
}
