namespace Infrastructure.Entities
{
    public class Category:BaseEntity
    {
        public int IdCategory { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<Dish> Dishes { get; set; }
    }
}