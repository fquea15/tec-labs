using AutoMapper;
using Lab11.Application.DTOs.Role;
using Lab11.Domain.Entities;
using Lab11.Domain.Interfaces;
using MediatR;

namespace Lab11.Application.UseCases.Roles.Queries;

public class GetRoleByIdQuery : IRequest<GetRoleDto>
{
    public Guid Id { get; set; }
}

internal class GetRoleByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetRoleByIdQuery, GetRoleDto>
{
    public async Task<GetRoleDto> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var role = await unitOfWork.GenericRepository<Role>().GetByIdAsync(request.Id);
        if (role == null)
            throw new InvalidOperationException("Rol no encontrado");

        return mapper.Map<GetRoleDto>(role);
    }
}