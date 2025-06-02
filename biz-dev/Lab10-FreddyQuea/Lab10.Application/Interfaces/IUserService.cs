using Lab10.Application.DTOs.Auth;
using Lab10.Application.DTOs.User;

namespace Lab10.Application.Interfaces;

public interface IUserService
{
    Task<GetUserDto> CreateUserAsync(CreateUserDto createUserDto);
    Task<GetUserDto> GetUserByIdAsync(Guid id);
    Task<IEnumerable<GetUserDto>> GetAllUsersAsync();
    Task<GetUserDto> GetUserWithTicketsAsync(Guid id);
}