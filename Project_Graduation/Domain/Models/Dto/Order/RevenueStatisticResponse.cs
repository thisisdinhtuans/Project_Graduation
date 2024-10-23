using System;

namespace Domain.Models.Dto.Order;

public class RevenueStatisticResponse
{
    public string TimePeriod { get; set; }
    public double Revenue { get; set; }
}
