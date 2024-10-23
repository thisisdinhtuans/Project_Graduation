using System;

namespace Domain.Models.Dto.Order;

public class OrderStatisticResponse
{
    public string TimePeriod { get; set; }
    public int OrderCount { get; set; }
}
