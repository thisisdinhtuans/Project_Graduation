using System;
using Domain.Enums;
using Domain.Models.Common.ApiResult;
using Domain.Models.Dto.Order;
using Domain.Models.Dto.OrderDetail;

namespace Infrastructure.Services.OrderService;

public class OrderService : IOrderService
{
    public Task<bool> ArrangeTableToOrder(int orderId, int tableId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AssignTableToOrder(int orderId, int tableId)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResult<OrderDto>> CreateOrder(OrderDto request)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResult<OrderDetailDto>> CreateOrderDetail(OrderDetailDto request)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResult<bool>> DeleteDishFromOrderDetail(int orderId, int orderDetailId, int dishId)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResult<bool>> DeleteOrder(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResult<bool>> DeleteOrderDetail(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResult<List<OrderDto>>> GetAllOrder()
    {
        throw new NotImplementedException();
    }

    public Task<ApiResult<OrderDto>> GetOrderById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResult<OrderDetailDto>> GetOrderDetailById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResult<bool>> UpdateOrder(int id, OrderDto request)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResult<bool>> UpdateOrderDetail(int id, OrderDetailDto request)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateOrderDetailsAsync(OrderDetailUpdateRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResult<bool>> UpdateOrderStatus(int orderId, EnumOrder newStatus)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResult<List<OrderDto>>> ViewOrderHistory(string username)
    {
        throw new NotImplementedException();
    }
}
