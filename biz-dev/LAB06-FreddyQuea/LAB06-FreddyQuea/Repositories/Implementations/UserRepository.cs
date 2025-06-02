using LAB06_FreddyQuea.Data;
using LAB06_FreddyQuea.Models;
using LAB06_FreddyQuea.Utilities;
using LAB06_FreddyQuea.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LAB06_FreddyQuea.Repositories.Implementations;

public class UserRepository(AppDbContext context) : GenericRepository<User>(context), IUserRepository
{
    private readonly AppDbContext _context = context;

    public async Task<ServiceResponse<User>> GetByUsernameAsync(string username)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        return user == null
            ? new ServiceResponse<User>(false, "Usuario no encontrado")
            : new ServiceResponse<User>(true, "Usuario encontrado", user);
    }
}