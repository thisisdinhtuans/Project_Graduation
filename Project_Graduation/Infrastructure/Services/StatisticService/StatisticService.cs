using System;
using Domain.Models.Dto.Order;

namespace Infrastructure.Services.StatisticService;

public class StatisticService : IStatisticService
{
    public Task<List<CustomerStatisticResponse>> GetCustomersStatisticAsync(int year, int? month, int? restaurantId)
    {
        throw new NotImplementedException();
    }

    public Task<List<OrderStatisticResponse>> GetOrdersStatisticAsync(int year, int? month, int? restaurantId, int? status)
    {
        throw new NotImplementedException();
    }

    public Task<List<RevenueStatisticResponse>> GetRevenuesStatisticResponsesAsync(int year, int? month, int? restaurantId)
    {
        throw new NotImplementedException();
    }
}
