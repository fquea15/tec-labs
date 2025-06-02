using Lab10.Application.DTOs.Role;

namespace Lab10.Application.Interfaces;

public interface IRoleService
{
    Task<GetRoleDto> CreateRoleAsync(CreateRoleDto createRoleDto);
    Task<IEnumerable<GetRoleDto>> GetAllRolesAsync();
}