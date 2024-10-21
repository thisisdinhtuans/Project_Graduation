using System;
using Domain.Enums;
using Domain.Models.Common.ApiResult;
using Domain.Models.Dto.Order;
using Domain.Models.Dto.OrderDetails;
using Infrastructure.Entities;

namespace Infrastructure.Services.OrderService;

public interface IOrderService
{
    Task<ApiResult<bool>> CreateOrder(OrderDto orderDto);
    Task<ApiResult<bool>> UpdateOrder(OrderDto orderDto);
    Task<ApiResult<bool>> DeleteOrder(int id);
    Task<ApiResult<OrderDto>> GetOrderById(int id);
    Task<ApiResult<List<OrderDto>>> GetAllOrder();
    Task<ApiResult<bool>> UpdateOrderStatus(int orderId, EnumOrder newStatus);
    Task<bool> ArrangeTableToOrder(int orderId, int tableId);
    Task<bool> AssignTableToOrder(int orderId, int tableId);
    // Task<ApiResult<bool>> DeleteDishFromOrderDetail(int orderId, int orderDetailId, int dishId);
    Task<ApiResult<List<OrderDto>>> ViewOrderHistory(string username);
}
