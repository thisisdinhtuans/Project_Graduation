using System;
using Domain.Models.Dto.Order;
using Infrastructure.Entities;
using Infrastructure.Repositories.BaseRepository;

namespace Infrastructure.Repositories.OrderRepository;

public interface IOrderRepository : IBaseRepository<Order>
    {
        Task<Order> CreateAsyncFLByOrder(Order entity);
        public Task<bool> DeleteDishFromOrderDetail(int orderId, int orderDetailId, int dishId);
        public Task<List<Order>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate);
        public Task<List<Order>> GetOrdersByRestaurantAndDateRangeAsync(int restaurantId, DateTime startDate, DateTime endDate);
        public Task<List<OrderDto>> GetOrdersByUsernameAsync(string username);
    }
