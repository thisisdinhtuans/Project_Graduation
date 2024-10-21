using System;
using AutoMapper;
using Domain.Models.Common;
using Domain.Models.Common.ApiResult;
using Domain.Models.Dto.OrderDetails;
using Infrastructure.Entities;
using Infrastructure.Repositories.OrderDetailRepository;
using Infrastructure.Repositories.OrderRepository;

namespace Infrastructure.Services.OrderDetailService;

public class OrderDetailService: IOrderDetailService
{
    private readonly IOrderDetailRepository _orderDetailRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public OrderDetailService(IOrderDetailRepository orderDetailRepository,IOrderRepository orderRepository, IMapper mapper)
    {
        _orderDetailRepository = orderDetailRepository;
        _orderRepository = orderRepository;
        _mapper = mapper;
    }
    public async Task<ApiResult<bool>> CreateOrderDetailAsync(OrderDetailDto orderDetailDto)
    {
        var order =await _orderRepository.GetByIdAsync(orderDetailDto.OrderId);
        if(order==null)
        {
            return new ApiErrorResult<bool>("Đơn hàng này không tồn tại");
        }
        if (orderDetailDto == null)
        {
            throw new ArgumentNullException(nameof(orderDetailDto));
        }

        // var addressExists = await _orderDetailRepository.AnyAsync(x => x.OrderDetailName == orderDetailDto.OrderDetailName);
        // if (addressExists)
        // {
        //     return new ApiErrorResult<bool>("Khu vực này đã tồn tại.");
        // }


        var orderDetail = _mapper.Map<OrderDetail>(orderDetailDto);
        // orderDetail.CreatedBy=_httpContextAccessor.HttpContext.User.Identity.Name;
        // orderDetail.CreatedDate=DateTime.Now;
        try
        {
            await _orderDetailRepository.Add(orderDetail);
            return new ApiSuccessResult<bool>(true);
        }
        catch (Exception ex)
        {
            return new ApiErrorResult<bool>(ex.Message);
        }

    }

    public async Task<bool> UpdateOrderDetailAsync(OrderDetailUpdateRequest orderDetailDto)
    {
        // var addressExists = await _orderDetailRepository.AnyAsync(x => x.OrderDetailName == orderDetailDto.OrderDetailName);
        //     if (addressExists)
        //     {
        //         return new ApiErrorResult<bool>("Khu vực  này đã tồn tại.");
        //     }

        var orderDetail = await _orderDetailRepository.GetByIdAsync(orderDetailDto.OrderId);
        if (orderDetail == null) throw new Exception("OrderDetail not found");

        _mapper.Map(orderDetailDto, orderDetail);
        // orderDetail.UpdatedDate = DateTime.Now;
        // orderDetail.UpdatedBy = _httpContextAccessor.HttpContext.User.Identity.Name;
        try
        {
            await _orderDetailRepository.Update(orderDetail);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<ApiResult<bool>> DeleteOrderDetailAsync(int id)
    {
        var orderDetail = await _orderDetailRepository.GetByIdAsync(id);
        if (orderDetail == null)
        {
            return new ApiErrorResult<bool>("Khu vực không được tìm thấy.");
        }

        try
        {
            await _orderDetailRepository.Delete(orderDetail);
            return new ApiSuccessResult<bool>(true);
        }
        catch (Exception ex)
        {
            return new ApiErrorResult<bool>(ex.Message);
        }
    }


    public async Task<ApiResult<OrderDetail>> GetOrderDetailByIdAsync(int id)
    {
        var orderDetail= await _orderDetailRepository.GetByIdAsync(id);
        return new ApiSuccessResult<OrderDetail>(orderDetail);
    }

    public async Task<ApiResult<List<OrderDetailDto>>> GetAllOrderDetailsAsync()
    {
        var orderDetails = await _orderDetailRepository.GetAllAsync(); // Lấy tất cả nhà hàng từ Repository
        var orderDetailsDto=_mapper.Map<List<OrderDetailDto>>(orderDetails); // Chuyển đổi sang DTO
        return new ApiSuccessResult<List<OrderDetailDto>>(orderDetailsDto);
    }

    public async Task<ApiResult<List<OrderDetailDto>>> GetAllByOrderId(int orderId)
    {
        var orderDetail=await _orderDetailRepository.GetByCondition(x=>x.OrderId==orderId);
        if(orderDetail==null || !orderDetail.Any())
        {
            return new ApiErrorResult<List<OrderDetailDto>>("Không tìm thấy chi tiết đơn hàng của Order này");
        }

        var data=_mapper.Map<List<OrderDetailDto>>(orderDetail.ToList());
        return new ApiSuccessResult<List<OrderDetailDto>>(data);
    }
}

