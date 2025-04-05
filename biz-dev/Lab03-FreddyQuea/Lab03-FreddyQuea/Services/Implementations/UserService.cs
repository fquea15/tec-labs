using AutoMapper;
using Lab03_FreddyQuea.DTOs;
using Lab03_FreddyQuea.DTOs.User;
using Lab03_FreddyQuea.Models;
using Lab03_FreddyQuea.Repositories.Interfaces;
using Lab03_FreddyQuea.Services.Interfaces;

namespace Lab03_FreddyQuea.Services.Implementations;

public class UserService(IGenericRepository<User> generic, IMapper mapper) : IUserService
{
    public async Task<IEnumerable<GetUser>> GetAllAsync()
    {
        var rawData = await generic.GetAll();
        return !rawData.Any() ? [] : mapper.Map<IEnumerable<GetUser>>(rawData);
    }

    public async Task<GetUser> GetByIdAsync(int id)
    {
        var rawData = await generic.GetById(id);
        return mapper.Map<GetUser>(rawData);
    }

    public async Task<ServiceResponse> AddAsync(CreateUser user)
    {
        var mappedData = mapper.Map<User>(user);
        var result = await generic.Add(mappedData);
        return result > 0
            ? new ServiceResponse(true, "User Added!")
            : new ServiceResponse(false, "User failed to be added!");
    }

    public async Task<ServiceResponse> UpdateAsync(UpdateUser user)
    {
        var mappedData = mapper.Map<User>(user);
        var result = await generic.Update(mappedData);
        return result > 0
            ? new ServiceResponse(true, "User Updated!")
            : new ServiceResponse(false, "User failed to be updated!");
    }

    public async Task<ServiceResponse> DeleteAsync(int id)
    {
        var result = await generic.Delete(id);

        return result > 0
            ? new ServiceResponse(true, "User Deleted!")
            : new ServiceResponse(false, "User not found or user to deleted.");
    }
}