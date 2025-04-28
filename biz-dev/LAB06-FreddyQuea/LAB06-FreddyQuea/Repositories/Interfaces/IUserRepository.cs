using LAB06_FreddyQuea.Models;
using LAB06_FreddyQuea.Utilities;

namespace LAB06_FreddyQuea.Repositories.Interfaces;

public interface IUserRepository : IGenericRepository<User>
{
    Task<ServiceResponse<User>> GetByUsernameAsync(string username);
}