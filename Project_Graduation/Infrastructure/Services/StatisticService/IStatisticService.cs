using System;
using Domain.Models.Dto.Order;

namespace Infrastructure.Services.StatisticService;

public interface IStatisticService
{
    Task<List<OrderStatisticResponse>> GetOrdersStatisticAsync(int year, int? month, int? restaurantId, int? status);
    Task<List<RevenueStatisticResponse>> GetRevenuesStatisticResponsesAsync(int year, int? month,int? restaurantId);
    Task<List<CustomerStatisticResponse>> GetCustomersStatisticAsync(int year, int? month,int? restaurantId);
}
