using Lab11.Application.DTOs.Role;

namespace Lab11.Application.DTOs.User;

public class GetUserDto
{
    public Guid UserId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public List<GetRoleDto> Roles { get; set; }
}