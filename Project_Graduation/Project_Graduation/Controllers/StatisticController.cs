using System;
using Infrastructure.Services.StatisticService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Project_Graduation.Controllers;

[Authorize(Roles = "Manager,Owner")]
public class StatisticController : BaseApiController
{
    private readonly IStatisticService _statisticsService;

    public StatisticController(IStatisticService statisticsService)
    {
        _statisticsService = statisticsService;
    }

    [HttpGet("orders/statistics")]
    public async Task<IActionResult> GetOrdersStatistics(int year, int? month = null, int? restaurantId = null, int? status = null)
    {
        var result = await _statisticsService.GetOrdersStatisticAsync(year, month, restaurantId,status);
        return Ok(result);
    }

    [HttpGet("revenue/statistics")]
    public async Task<IActionResult> GetRevenueStatistics(int year, int? month = null, int? restaurantId = null)
    {
        var result = await _statisticsService.GetRevenuesStatisticResponsesAsync(year, month, restaurantId);
        return Ok(result);
    }
    [HttpGet("customer/statistics")]
    public async Task<IActionResult> GetCustomerStatistics(int year, int? month = null, int? restaurantId = null)
    {
        var result = await _statisticsService.GetCustomersStatisticAsync(year, month, restaurantId);
        return Ok(result);
    }
}
