using System;

namespace Domain.Models.Dto.OrderDetails;

public class OrderDetailUpdateRequest
{
    public int OrderId { get; set; }
    public List<OrderDetailDto> OrderDetails { get; set; }
}
