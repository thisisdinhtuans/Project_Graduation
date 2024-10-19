using System;

namespace Domain.Models.Dto.OrderDetail;

public class OrderDetailUpdateRequest
{
    public int OrderID { get; set; }
    public List<OrderDetailDto> OrderDetails { get; set; }
}
