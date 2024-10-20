using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Dto.User;

public class UserRequestDto
{
    public Guid Id { get; set; }

        [Display(Name = "Ho va Ten")]
        public string? FullName { get; set; }


        [Display(Name = "SDT")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "TK")]
        public string? UserName { get; set; }

        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Display(Name = "Ngay sinh")]
        public DateTime? Dob { get; set; }
        [Display(Name = "Gioi Tinh")]
        public bool? Gender { get; set; }
        [Display(Name = "Can Cuoc Cong Dan")]
        public string? CCCD { get; set; }

        public string PassWord { get; set; }
        public IList<string>? Roles { get; set; }
        public List<string>? Opes { get; set; }

        public int RestaurantID { get; set; }
        public int Status { get; set; }
}
