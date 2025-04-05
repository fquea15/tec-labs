using Lab03_FreddyQuea.DTOs;
using Lab03_FreddyQuea.DTOs.User;

namespace Lab03_FreddyQuea.Services.Interfaces;

public interface IUserService
{
    Task<IEnumerable<GetUser>> GetAllAsync();
    Task<GetUser> GetByIdAsync(int id);
    Task<ServiceResponse> AddAsync(CreateUser user);
    Task<ServiceResponse> UpdateAsync(UpdateUser user);
    Task<ServiceResponse> DeleteAsync(int id);
}