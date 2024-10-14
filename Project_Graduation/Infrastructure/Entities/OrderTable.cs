namespace Infrastructure.Entities
{
    public class OrderTable:BaseEntity
    {
        public int TableID { get; set; }
        public Table Table { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}