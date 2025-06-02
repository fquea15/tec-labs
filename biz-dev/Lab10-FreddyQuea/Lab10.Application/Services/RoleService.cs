using AutoMapper;
using Lab10.Application.DTOs.Role;
using Lab10.Application.Interfaces;
using Lab10.Domain.Entities;
using Lab10.Domain.Interfaces;

namespace Lab10.Application.Services;

public class RoleService(IUnitOfWork unitOfWork, IMapper mapper) : IRoleService
{
    public async Task<GetRoleDto> CreateRoleAsync(CreateRoleDto createRoleDto)
    {
        var existingRole = (await unitOfWork.GenericRepository<Role>().GetAllAsync())
            .FirstOrDefault(r => r.RoleName == createRoleDto.RoleName);
        if (existingRole != null)
            throw new Exception($"Role {createRoleDto.RoleName} ya existe");

        var role = mapper.Map<Role>(createRoleDto);
        role.Id = Guid.NewGuid();

        await unitOfWork.GenericRepository<Role>().AddAsync(role);
        await unitOfWork.CompleteAsync();

        return mapper.Map<GetRoleDto>(role);
    }

    public async Task<IEnumerable<GetRoleDto>> GetAllRolesAsync()
    {
        var roles = await unitOfWork.GenericRepository<Role>().GetAllAsync();
        return mapper.Map<IEnumerable<GetRoleDto>>(roles);
    }
}