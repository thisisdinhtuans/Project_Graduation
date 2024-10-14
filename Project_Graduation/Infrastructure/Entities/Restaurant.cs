namespace Infrastructure.Entities
{
    public class Restaurant:BaseEntity
    {
        public int RestaurantID { get; set; }
        public string Address { get; set; }=string.Empty;
        public ICollection<Area> Areas { get; set; }
    }
}