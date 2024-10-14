namespace Infrastructure.Entities
{
    public class Area : BaseEntity
    {
        public int AreaID { get; set; }
        public string AreaName { get; set; }=string.Empty;
        public int RestaurantID { get; set; }
        public Restaurant Restaurant { get; set; }
        public ICollection<Table> Tables { get; set; }
    }
}