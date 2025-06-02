using AutoMapper;
using Lab11.Application.DTOs.User;
using Lab11.Domain.Entities;
using Lab11.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Lab11.Application.UseCases.Users.Queries;

public class GetAllUsersQuery : IRequest<IEnumerable<GetUserDto>>
{
}

internal class GetAllUsersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetAllUsersQuery, IEnumerable<GetUserDto>>
{
    public async Task<IEnumerable<GetUserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await unitOfWork.GenericRepository<User>()
            .GetAllWithIncludesAsync(query => query
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role));
        return mapper.Map<IEnumerable<GetUserDto>>(users);
    }
}