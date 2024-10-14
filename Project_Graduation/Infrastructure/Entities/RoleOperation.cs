namespace Infrastructure.Entities
{
    public class RoleOperation : BaseEntity
    {
        public int Id { get; set; }
        public string? RoleId { get; set; }
        public int OperationId { get; set; }
        public bool IsAccess { get; set; }
    }
}