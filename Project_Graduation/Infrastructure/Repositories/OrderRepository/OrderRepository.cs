using System;
using Domain.Models.Dto.Order;
using Infrastructure.Data;
using Infrastructure.Entities;
using Infrastructure.Repositories.AuditRepository;
using Infrastructure.Repositories.BaseRepository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.OrderRepository;

public class OrderRepository: BaseRepository<Order>, IOrderRepository
    {
        private readonly Project_Graduation_Context _db;

        public OrderRepository(Project_Graduation_Context dbContext, IAuditRepository<Order> auditRepository) : base(dbContext, auditRepository)
        {
            _db = dbContext;
        }

        public async Task<Order> CreateAsyncFLByOrder(Order entity)
        {
            _db.Orders.Add(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
        public async Task<bool> DeleteDishFromOrderDetail(int orderId, int orderDetailId, int dishId)
        {
            var orderDetail = await _db.OrderDetails
                .FirstOrDefaultAsync(od => od.OrderId == orderId && od.Id == orderDetailId && od.DishId == dishId);

            if (orderDetail == null)
            {
                return false;
            }

            _db.OrderDetails.Remove(orderDetail);
            await _db.SaveChangesAsync();
      
            var order = await _db.Orders
                .Include(o => o.OrderDetails)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (order != null)
            {
                order.PriceTotal = order.OrderDetails.Sum(od => od.Price * od.Quantity);
                await _db.SaveChangesAsync();
            }

            return true;
        }
        public async Task<List<Order>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _db.Orders
                .Where(o => o.Date >= startDate && o.Date <= endDate)
                .ToListAsync();
        }
        public async Task<List<Order>> GetOrdersByRestaurantAndDateRangeAsync(int restaurantId, DateTime startDate, DateTime endDate)
        {
            return await _db.Orders
                .Where(o => o.RestaurantID == restaurantId && o.Date >= startDate && o.Date <= endDate)
                .ToListAsync();
        }
        public async Task<List<OrderDto>> GetOrdersByUsernameAsync(string username)
        {
            return await _db.Orders
                .Where(o => o.UserName == username)
                .Select(o => new OrderDto
                {
                    OrderID = o.OrderId,
                    UserName = o.UserName,
                    Date = o.Date,
                    PriceTotal = o.PriceTotal,
                    NumberOfCustomer = o.NumberOfCustomer,
                    Status = o.Status,
                    RestaurantID = o.RestaurantID,
                    Phone = o.Phone,
                    Deposit = o.Deposit,
                    TableID = o.TableID,
                    VAT = o.VAT,
                    Time = o.Time,
                    Payment = o.Payment,
                    Description = o.Description,
                })
                .ToListAsync();
        }
}