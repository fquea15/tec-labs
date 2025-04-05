using Lab03_FreddyQuea.Data;
using Lab03_FreddyQuea.Models;
using Lab03_FreddyQuea.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lab03_FreddyQuea.Repositories.Implementations;

public class UserRepository(ApplicationDbContext context) : GenericRepository<User>(context), IUserRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<User?> GetUserByEmailAsync(string email) =>
        await _context.Users.FirstOrDefaultAsync(u => u != null && u.Email == email);
}

