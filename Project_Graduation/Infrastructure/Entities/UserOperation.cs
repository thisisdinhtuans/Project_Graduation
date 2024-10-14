namespace Infrastructure.Entities
{
    public class UserOperation:BaseEntity
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public long OperationId { get; set; }
        public bool IsAccess { get; set; }
    }
}