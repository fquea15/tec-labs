using AutoMapper;
using Lab11.Application.DTOs.User;
using Lab11.Domain.Entities;
using Lab11.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Lab11.Application.UseCases.Users.Queries;

public class GetUserByIdQuery : IRequest<GetUserDto>
{
    public Guid UserId { get; init; }
}

internal sealed class GetUserByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetUserByIdQuery, GetUserDto>
{
    public async Task<GetUserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.GenericRepository<User>()
            .GetByIdWithIncludesAsync(request.UserId, query => query.Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role));
        if (user == null) throw new Exception("Usuario no encontrado");

        return mapper.Map<GetUserDto>(user);
    }
}