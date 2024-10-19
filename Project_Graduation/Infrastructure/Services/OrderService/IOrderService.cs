using System;
using Domain.Enums;
using Domain.Models.Common.ApiResult;
using Domain.Models.Dto.Order;
using Domain.Models.Dto.OrderDetail;

namespace Infrastructure.Services.OrderService;

public interface IOrderService
{
    public Task<ApiResult<OrderDto>> CreateOrder(OrderDto request);
    public Task<ApiResult<OrderDetailDto>> CreateOrderDetail(OrderDetailDto request);
    public Task<ApiResult<bool>> UpdateOrder(int id, OrderDto request);

    public Task<ApiResult<bool>> UpdateOrderDetail(int id, OrderDetailDto request);

    public Task<ApiResult<bool>> DeleteOrder(int id);

    public Task<ApiResult<bool>> DeleteOrderDetail(int id);

    public Task<ApiResult<OrderDto>> GetOrderById(int id);
    public Task<ApiResult<OrderDetailDto>> GetOrderDetailById(int id);
    public Task<ApiResult<List<OrderDto>>> GetAllOrder();
    public Task<ApiResult<bool>> UpdateOrderStatus(int orderId, EnumOrder newStatus);
    public Task<bool> ArrangeTableToOrder(int orderId, int tableId);

    public Task<bool> AssignTableToOrder(int orderId, int tableId);

    public Task<bool> UpdateOrderDetailsAsync(OrderDetailUpdateRequest request);

    public Task<ApiResult<bool>> DeleteDishFromOrderDetail(int orderId, int orderDetailId, int dishId);
    Task<ApiResult<List<OrderDto>>> ViewOrderHistory(string username);
}
