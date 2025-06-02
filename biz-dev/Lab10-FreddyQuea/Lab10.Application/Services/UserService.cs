using AutoMapper;
using Lab10.Application.DTOs.User;
using Lab10.Application.Interfaces;
using Lab10.Domain.Entities;
using Lab10.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Lab10.Application.Services;

public class UserService(IUnitOfWork unitOfWork, IMapper mapper) : IUserService
{
    public async Task<GetUserDto> CreateUserAsync(CreateUserDto createUserDto)
    {
        var user = mapper.Map<User>(createUserDto);
        user.Id = Guid.NewGuid();

        await unitOfWork.GenericRepository<User>().AddAsync(user);

        foreach (var roleType in createUserDto.Roles)
        {
            var role = (await unitOfWork.GenericRepository<Role>().GetAllAsync())
                .FirstOrDefault(r => r.RoleName == roleType);
            if (role == null)
            {
                role = new Role
                {
                    Id = Guid.NewGuid(),
                    RoleName = roleType
                };
                await unitOfWork.GenericRepository<Role>().AddAsync(role);
            }

            var userRole = new UserRole
            {
                UserId = user.Id,
                RoleId = role.Id,
                AssignedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified)
            };
            await unitOfWork.GenericRepository<UserRole>().AddAsync(userRole);
        }

        await unitOfWork.CompleteAsync();

        var createdUser = await unitOfWork.GenericRepository<User>()
            .GetByIdWithIncludesAsync(user.Id, query => query
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role));
        var userDto = mapper.Map<GetUserDto>(createdUser);
        return userDto;
    }

    public async Task<GetUserDto> GetUserByIdAsync(Guid id)
    {
        var user = await unitOfWork.GenericRepository<User>()
            .GetByIdWithIncludesAsync(id, query => query
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role));
        if (user == null)
            throw new Exception("Usuario no encontrado");
        return mapper.Map<GetUserDto>(user);
    }

    public async Task<IEnumerable<GetUserDto>> GetAllUsersAsync()
    {
        var users = await unitOfWork.GenericRepository<User>()
            .GetAllWithIncludesAsync(query => query
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role));
        return mapper.Map<IEnumerable<GetUserDto>>(users);
    }

    public async Task<GetUserDto> GetUserWithTicketsAsync(Guid id)
    {
        var user = await unitOfWork.GenericRepository<User>()
            .GetByIdWithIncludesAsync(id, query => query
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .Include(u => u.Tickets)
                .ThenInclude(t => t.Responses));
        if (user == null)
            throw new Exception("Usuario no encontrado");
        return mapper.Map<GetUserDto>(user);
    }
}