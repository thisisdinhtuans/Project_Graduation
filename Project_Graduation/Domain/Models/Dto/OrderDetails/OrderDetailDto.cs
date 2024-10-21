using System;

namespace Domain.Models.Dto.OrderDetails;

public class OrderDetailDto
{
    public int Id { get; set; }
    public double Price { get; set; }
    public string Description { get; set; }=string.Empty;
    public int DishId { get; set; }
    public int NumberOfCustomer { get; set; }
    public int Quantity { get; set; }
    public int OrderId { get; set; }
}
