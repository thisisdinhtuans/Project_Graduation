using System;
using Domain.Models.Dto.Order;
using Infrastructure.Entities;
using Infrastructure.Repositories.OrderRepository;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services.StatisticService;

public class StatisticService : IStatisticService
{
    private readonly IOrderRepository _orderRepository;

    public StatisticService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<List<CustomerStatisticResponse>> GetCustomersStatisticAsync(int year, int? month, int? restaurantId)
    {
        DateTime startDate, endDate;

        if(month.HasValue)
        {
            startDate=new DateTime(year, month.Value,1);
            endDate=startDate.AddMonths(1).AddDays(-1);
        }
        else 
        {
            startDate=new DateTime(year, 1, 1);
            endDate=new DateTime(year, 12, 31);
        }

        List<Order> orders;

        if(restaurantId.HasValue)
        {
            orders = await _orderRepository.GetOrdersByRestaurantAndDateRangeAsync(restaurantId.Value, startDate, endDate);
        }
        else {
            orders=await _orderRepository.GetOrdersByDateRangeAsync(startDate, endDate);
        }

        var statistics=orders
            .GroupBy(o=>month.HasValue ? o.Date.Date : new DateTime(o.Date.Year, o.Date.Month, 1))
            .Select(g=> new CustomerStatisticResponse
            {
                TimePeriod=month.HasValue ? g.Key.ToString("yyyy-MM-dd") : g.Key.ToString("yyyy-MM"),
                NumberCustomer=g.Sum(o=>o.NumberOfCustomer)
            })
            .ToList();
        return statistics;
    }

    public async Task<List<OrderStatisticResponse>> GetOrdersStatisticAsync(int year, int? month, int? restaurantId, int? status)
    {
        DateTime startDate, endDate;

        if(month.HasValue)
        {
            startDate=new DateTime(year,  month.Value, 1);
            endDate=startDate.AddMonths(1).AddDays(-1);
        } else {
            startDate=new DateTime(year, 1,1);
            endDate=new DateTime(year, 12,31);
        }

        List<Order> orders;

        if(restaurantId.HasValue)
        {
            orders=await _orderRepository.GetOrdersByRestaurantAndDateRangeAsync(restaurantId.Value, startDate, endDate);
        } else {
            orders=await _orderRepository.GetOrdersByDateRangeAsync(startDate, endDate);
        }

        if(status.HasValue)
        {
            orders=orders.Where(o=>o.Status==status.Value).ToList();
        }

        var statistics=orders
            .GroupBy(o=>month.HasValue ? o.Date.Date:new DateTime(o.Date.Year, o.Date.Month, 1))
            .Select(g=>new OrderStatisticResponse
            {
                TimePeriod=month.HasValue ? g.Key.ToString("yyyy-MM-dd") : g.Key.ToString("yyyy-MM"),
                OrderCount=g.Count()
            })
            .ToList();
        return statistics;
    }

    public async Task<List<RevenueStatisticResponse>> GetRevenuesStatisticResponsesAsync(int year, int? month, int? restaurantId)
    {
        DateTime startDate, endDate;

        if (month.HasValue)
        {
            startDate = new DateTime(year, month.Value, 1);
            endDate = startDate.AddMonths(1).AddDays(-1);
        }
        else
        {
            startDate = new DateTime(year, 1, 1);
            endDate = new DateTime(year, 12, 31);
        }

        List<Order> orders;

        if (restaurantId.HasValue)
        {
            orders = await _orderRepository.GetOrdersByRestaurantAndDateRangeAsync(restaurantId.Value, startDate, endDate);
        }
        else
        {
            orders = await _orderRepository.GetOrdersByDateRangeAsync(startDate, endDate);
        }

        var statistics = orders
            .GroupBy(o => month.HasValue ? o.Date.Date : new DateTime(o.Date.Year, o.Date.Month, 1))
            .Select(g => new RevenueStatisticResponse
            {
                TimePeriod = month.HasValue ? g.Key.ToString("yyyy-MM-dd") : g.Key.ToString("yyyy-MM"),
                Revenue = g.Sum(o => o.PriceTotal)
            })
            .ToList();

        return statistics;
    }
}
