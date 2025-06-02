using Lab11.Domain.Entities;

namespace Lab11.Application.Interfaces;

public interface IAuthService
{
    string GenerateJwtToken(User user);
}