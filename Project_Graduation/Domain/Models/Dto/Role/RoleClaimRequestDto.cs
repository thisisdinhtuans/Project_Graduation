using System;

namespace Domain.Models.Dto.Role;

public class RoleClaimRequestDto
{
    public string RoleName { get; set; }
    public string ClaimType { get; set; }
    public string ClaimValue { get; set; }
}
