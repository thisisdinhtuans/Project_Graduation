using System;
using Domain.Models.Dto.OrderDetails;

namespace Domain.Models.Dto.Order;

public class OrderDetailUpdateRequest
{
    public int OrderID { get; set; }
    public List<OrderDetailDto> OrderDetails { get; set; }
}
