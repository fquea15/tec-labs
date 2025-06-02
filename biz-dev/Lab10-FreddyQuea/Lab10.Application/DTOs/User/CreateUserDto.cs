using Lab10.Domain.Enum;

namespace Lab10.Application.DTOs.User;

public class CreateUserDto
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string? Email { get; set; }
    public List<RoleName> Roles { get; set; } = new();
}