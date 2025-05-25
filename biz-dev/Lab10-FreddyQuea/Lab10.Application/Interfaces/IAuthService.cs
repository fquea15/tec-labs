using Lab10.Application.DTOs.Auth;

namespace Lab10.Application.Interfaces;

public interface IAuthService
{
    Task<string> AuthenticateAsync(LoginDto loginDto);
}