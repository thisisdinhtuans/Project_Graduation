using System;

namespace Domain.Models.Dto.Order;

public class CustomerStatisticResponse
{
    public string TimePeriod { get; set; }
    public int NumberCustomer { get; set; }
}
