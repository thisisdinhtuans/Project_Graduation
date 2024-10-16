using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Dto;

public class BaseDto
{
        [MaxLength(300)]
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        [MaxLength(300)]
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
}
