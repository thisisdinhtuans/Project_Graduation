using System;
using Domain.Models.Dto.OrderDetails;

namespace Domain.Models.Dto.Order;

public class OrderDto
{
        public int OrderId { get; set; }
        public int RestaurantID { get; set; }
        public string UserName { get; set; }=string.Empty;
        public double PriceTotal { get; set; }
        public string Description { get; set; } = string.Empty;
        public int NumberOfCustomer { get; set; }
        public int TableID { get; set; }
        public double Payment { get; set; }
        public double VAT { get; set; }
        public string Phone { get; set; }   
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public int Status { get; set; }
        public bool Deposit { get; set; }
        public double? Discount { get; set; }
        public ICollection<OrderDetailDto>? OrderDetailDtos { get; set; }

}
