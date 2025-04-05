using Lab03_FreddyQuea.Models;

namespace Lab03_FreddyQuea.Repositories.Interfaces;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> GetUserByEmailAsync(string email);
}
