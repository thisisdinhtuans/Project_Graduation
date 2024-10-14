using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities
{
    public class BaseEntity
    {
        [MaxLength(300)]
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        [MaxLength(300)]
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }
}