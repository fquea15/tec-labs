using AutoMapper;
using Lab11.Application.DTOs.User;
using Lab11.Domain.Entities;
using Lab11.Domain.Interfaces;
using MediatR;

namespace Lab11.Application.UseCases.Users.Commands;

public class CreateUserCommand : IRequest<GetUserDto>
{
    public string Username { get; init; } = null!;
    public string Password { get; init; } = null!;
    public string Email { get; init; } = null!;
}

internal sealed class CreateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<CreateUserCommand, GetUserDto>
{
    public async Task<GetUserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var repo = unitOfWork.GenericRepository<User>();

        var exists = await repo.ExistsAsync(u =>
            u.Username == request.Username || u.Email == request.Email);

        if (exists)
            throw new InvalidOperationException("El nombre de usuario o correo ya existe");

        var user = mapper.Map<User>(request);
        user.UserId = Guid.NewGuid();

        await repo.AddAsync(user);
        await unitOfWork.CompleteAsync();

        return mapper.Map<GetUserDto>(user);
    }
}