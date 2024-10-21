using System;
using Domain.Models.Common.ApiResult;
using Domain.Models.Dto.OrderDetails;
using Infrastructure.Entities;

namespace Infrastructure.Services.OrderDetailService;

public interface IOrderDetailService
{
    Task<ApiResult<bool>> CreateOrderDetailAsync(OrderDetailDto orderDetailDto);
    Task<bool> UpdateOrderDetailAsync(OrderDetailUpdateRequest orderDetailDto);
    Task<ApiResult<bool>> DeleteOrderDetailAsync(int id);
    Task<ApiResult<OrderDetail>> GetOrderDetailByIdAsync(int id);
    Task<ApiResult<List<OrderDetailDto>>> GetAllOrderDetailsAsync();
    Task<ApiResult<List<OrderDetailDto>>> GetAllByOrderId(int orderId);
}
