using System;
using Domain.Models.Page;

namespace Domain.Models.Dto.Role;

public class RoleAssignRequestDto
{
    public List<SelectItem> Roles { get; set; } = new List<SelectItem>();
}
