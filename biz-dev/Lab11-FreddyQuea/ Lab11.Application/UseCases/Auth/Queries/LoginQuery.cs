using Lab11.Application.Interfaces;
using Lab11.Domain.Entities;
using Lab11.Domain.Interfaces;
using MediatR;

namespace Lab11.Application.UseCases.Auth.Queries;

public class LoginQuery : IRequest<string>
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}

internal sealed class LoginQueryHandler(IUnitOfWork unitOfWork, IAuthService authService)
    : IRequestHandler<LoginQuery, string>
{
    public async Task<string> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var user = (await unitOfWork.GenericRepository<User>().GetAllAsync())
            .FirstOrDefault(u => u.Username == request.Username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            throw new InvalidOperationException("Credenciales inválidas");

        return authService.GenerateJwtToken(user);
    }
}