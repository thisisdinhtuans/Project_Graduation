using System;
using System.Security.Cryptography.X509Certificates;
using Domain.Models.Dto.Order;
using Infrastructure.Data;
using Infrastructure.Entities;
using Infrastructure.Repositories.AuditRepository;
using Infrastructure.Repositories.BaseRepository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.OrderRepository;

public class OrderRepository:BaseRepository<Order>, IOrderRepository
{
    private readonly Project_Graduation_Context _dbContext;

    public OrderRepository(Project_Graduation_Context dbContext, IAuditRepository<Order> auditRepository):base(dbContext, auditRepository)
    {
        _dbContext = dbContext;
    }

    public async Task<Order> CreateOrder(Order order)
    {
        _dbContext.Orders.Add(order);
        await _dbContext.SaveChangesAsync();
        return order;
    }

    public async Task<bool> DeleteDishFromOrderDetail(int orderId, int orderDetailId, int dishId)
    {
        var orderDetail=await _dbContext.OrderDetails.Where(o=>o.OrderId==orderId && o.Id==orderDetailId && o.DishId==dishId).FirstOrDefaultAsync();
        if(orderDetail==null)
        {
            return false;
        }

        _dbContext.OrderDetails.Remove(orderDetail);
        await _dbContext.SaveChangesAsync();

        var order=await _dbContext.Orders
            .Include(o=>o.OrderDetails)
            .Where(o=>o.OrderId==orderId)
            .FirstOrDefaultAsync();
        
        if(order!=null)
        {
            order.PriceTotal=order.OrderDetails.Sum(od=>od.Price * od.Quantity);
            await _dbContext.SaveChangesAsync();
        }
        return true;
    }

    public async Task<List<Order>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate) 
    {
        var orders=await _dbContext.Orders.Where(o=>o.Date>=startDate && o.Date<=endDate).ToListAsync();
        return orders;
    }

    public async Task<List<Order>> GetOrdersByRestaurantAndDateRangeAsync(int restaurantID, DateTime startDate, DateTime endDate)
    {
        var orders=await _dbContext.Orders.Where(o=>o.RestaurantID==restaurantID && o.Date>=startDate && o.Date<=endDate).ToListAsync();
        return orders;
    }


    // public async Task<List<OrderDto>> GetOrdersByUsernameAsync(string username)
    // {
    //     return await _dbContext.Orders.Where(o=>o.UserName==username).Select(
    //         o => new OrderDto 
    //         {
    //             OrderId=o.OrderId,
    //             RestaurantID=o.RestaurantID,
    //             PriceTotal=o.PriceTotal,
    //             Description=o.Description,
    //             NumberOfCustomer=o.NumberOfCustomer,
    //             TableID=o.TableID,
    //             Payment=o.Payment,
    //             VAT=o.VAT,
    //             Phone=o.Phone,
    //             Status=o.Status,
    //             Date=o.Date,
    //             Time=o.Time,
    //             Deposit=o.Deposit,
    //             Discount=o.Discount
    //         }
    //     ).ToListAsync();
    // }
}
