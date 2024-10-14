namespace Infrastructure.Entities
{
    public class Order:BaseEntity
    {
        public int OrderId { get; set; }
        public int RestaurantID { get; set; }
        public string UserName { get; set; }=string.Empty;
        public double PriceTotal { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int NumberOfCustomer { get; set; }
        public int TableID { get; set; }
        public double Payment { get; set; }
        public double VAT { get; set; }
        public string Phone { get; set; }   
        public int Status { get; set; }
        public bool Deposit { get; set; }
        public double? Discount { get; set; }
        public ICollection<OrderTable>? OrderTables { get; set; }
        public ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}