namespace Infrastructure.Entities
{
    public class Blog:BaseEntity
    {
        public int BlogID { get; set; }
        public string? Image {get;set; }
        public string? Title { get; set; }
        public string? SubTitle { get; set; }
        public string? Description { get; set; }
        public bool Status { get; set; }
    }
}