using System;
using Domain.Models.Dto.Order;
using Infrastructure.Entities;
using Infrastructure.Repositories.BaseRepository;

namespace Infrastructure.Repositories.OrderRepository;

public interface IOrderRepository:IBaseRepository<Order>
{
    Task<Order> CreateOrder(Order order);
    Task<bool> DeleteDishFromOrderDetail(int orderId, int orderDetailId, int dishId);
    Task<List<Order>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate);
    Task<List<Order>> GetOrdersByRestaurantAndDateRangeAsync(int restaurantID, DateTime startDate, DateTime endDate);
    // Task<List<OrderDto>> GetOrdersByUsernameAsync(string username);
}
