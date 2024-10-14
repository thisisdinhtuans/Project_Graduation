namespace Infrastructure.Entities
{
    public class Table:BaseEntity
    {
        public int TableID { get; set; }
        public int TableNumber { get; set; }
        public int Status { get; set; }
        public string NumberOfDesk { get; set; }
        public int AreaID { get; set; }
        public Area Area { get; set; }
        public ICollection<OrderTable> OrderTables { get; set; }
    }
}