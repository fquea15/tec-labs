using Lab10.Domain.Enum;

namespace Lab10.Application.DTOs.Role;

public class GetRoleDto
{
    public Guid Id { get; set; }
    public RoleName RoleName { get; set; }
}