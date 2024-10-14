using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities
{
    public class Operation:BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int ModuleId { get; set; }
        public string? Name { get; set; }
        public long Url { get; set; }
        public string? Code { get; set; }
        public bool IsShow { get; set; }
        public long Icon { get; set; }
    }
}