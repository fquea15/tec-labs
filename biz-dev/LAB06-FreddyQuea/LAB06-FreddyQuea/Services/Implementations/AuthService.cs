using LAB06_FreddyQuea.Repositories.Interfaces;
using LAB06_FreddyQuea.DTOs.Auth;
using LAB06_FreddyQuea.Utilities;
using LAB06_FreddyQuea.Exceptions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using LAB06_FreddyQuea.Models;
using LAB06_FreddyQuea.Services.Interfaces;
using AutoMapper;
using LAB06_FreddyQuea.Utilities.Interfaces;

namespace LAB06_FreddyQuea.Services.Implementations;

public class AuthService(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    IConfiguration configuration,
    IMapper mapper)
    : IAuthService
{
    public async Task<ServiceResponse<RegisterResponse>> RegisterAsync(RegisterRequest request)
    {
        var existingUser = await userRepository.GetByUsernameAsync(request.Username);
        if (existingUser.data != null)
        {
            return new ServiceResponse<RegisterResponse>(false, "El nombre de usuario ya está en uso.");
        }

        var hashedPassword = passwordHasher.HashPassword(request.Password);

        var user = new User
        {
            Username = request.Username,
            Password = hashedPassword,
            Role = request.Role
        };

        var result = await userRepository.AddAsync(user);

        if (result.data <= 0) return new ServiceResponse<RegisterResponse>(false, "No se pudo registrar el usuario.");
        var response = mapper.Map<RegisterResponse>(user);

        return new ServiceResponse<RegisterResponse>(true, "Usuario registrado correctamente", response);
    }

    public async Task<ServiceResponse<LoginResponse>> LoginAsync(LoginRequest request)
    {
        var user = await userRepository.GetByUsernameAsync(request.Username);
        if (user == null)
        {
            throw new ItemNotFoundException("El nombre de usuario no existe.");
        }

        if (!passwordHasher.VerifyPassword(request.Password, user.data?.Password!))
        {
            return new ServiceResponse<LoginResponse>(false, "Contraseña incorrecta.");
        }

        var token = GenerateJwtToken(user.data!);

        var response = new LoginResponse
        {
            Token = token,
            Expiration = DateTime.UtcNow.AddHours(1)
        };

        return new ServiceResponse<LoginResponse>(true, "Inicio de sesión exitoso", response);
    }

    public async Task<ServiceResponse<bool>> ChangePasswordAsync(ChangePasswordRequest request)
    {
        var user = await userRepository.GetByUsernameAsync(request.Username);
        if (user == null)
        {
            throw new ItemNotFoundException("El nombre de usuario no existe.");
        }

        if (!passwordHasher.VerifyPassword(request.CurrentPassword, user.data.Password))
        {
            return new ServiceResponse<bool>(false, "La contraseña actual es incorrecta.");
        }

        if (request.NewPassword != request.ConfirmNewPassword)
        {
            return new ServiceResponse<bool>(false, "Las nuevas contraseñas no coinciden.");
        }

        var newHashedPassword = passwordHasher.HashPassword(request.NewPassword);
        user.data!.Password = newHashedPassword;

        var result = await userRepository.UpdateAsync(user.data);
        return result.data > 0
            ? new ServiceResponse<bool>(true, "Contraseña cambiada correctamente.", true)
            : new ServiceResponse<bool>(false, "No se pudo cambiar la contraseña.");
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(configuration["Jwt:Key"] ?? throw new InvalidOperationException()));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}