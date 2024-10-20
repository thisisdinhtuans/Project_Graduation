using System;

namespace Domain.Models.Dto.Role;

public class RoleRequestDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}
