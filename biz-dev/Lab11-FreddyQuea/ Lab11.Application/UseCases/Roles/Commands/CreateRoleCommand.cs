using AutoMapper;
using Lab11.Application.DTOs.Role;
using Lab11.Domain.Entities;
using Lab11.Domain.Interfaces;
using MediatR;

namespace Lab11.Application.UseCases.Roles.Commands;

public class CreateRoleCommand : IRequest<GetRoleDto>
{
    public string Name { get; set; } = null!;
}

internal class CreateRoleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<CreateRoleCommand, GetRoleDto>
{
    public async Task<GetRoleDto> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = mapper.Map<Role>(request);
        role.RoleId = Guid.NewGuid();

        await unitOfWork.GenericRepository<Role>().AddAsync(role);
        await unitOfWork.CompleteAsync();

        return mapper.Map<GetRoleDto>(role);
    }
}