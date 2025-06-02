using LAB06_FreddyQuea.DTOs.Auth;
using LAB06_FreddyQuea.Utilities;

namespace LAB06_FreddyQuea.Services.Interfaces;

public interface IAuthService
{
    Task<ServiceResponse<RegisterResponse>> RegisterAsync(RegisterRequest request);
    Task<ServiceResponse<LoginResponse>> LoginAsync(LoginRequest request);
    Task<ServiceResponse<bool>> ChangePasswordAsync(ChangePasswordRequest request);
}