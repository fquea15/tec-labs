using AutoMapper;
using Lab11.Application.DTOs.Role;
using Lab11.Domain.Entities;
using Lab11.Domain.Interfaces;
using MediatR;

namespace Lab11.Application.UseCases.Roles.Queries;

public class GetAllRolesQuery : IRequest<IEnumerable<GetRoleDto>>
{
}

internal class GetAllRolesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetAllRolesQuery, IEnumerable<GetRoleDto>>
{
    public async Task<IEnumerable<GetRoleDto>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = await unitOfWork.GenericRepository<Role>().GetAllAsync();
        return mapper.Map<IEnumerable<GetRoleDto>>(roles);
    }
}