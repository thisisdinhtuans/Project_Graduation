using System;

namespace Domain.Models.Dto.OrderDetail;

public class OrderDetailDto
{
        public int OrderID { get; set; }
        public double Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public int DishID { get; set; }
        public DateTime Date { get; set; }
        public int NumberOfCustomer { get; set; }
        public int TableID { get; set; }
        public double Payment { get; set; }
        public int Quantity { get; set; }
        public int Id { get; set; }
        public int TableNumber { get; set; }
}
