using AutoMapper;
using Lab10.Application.DTOs.Response;
using Lab10.Application.DTOs.Role;
using Lab10.Application.DTOs.Ticket;
using Lab10.Application.DTOs.User;
using Lab10.Domain.Entities;
using Lab10.Domain.Enum;

namespace Lab10.Application.Mapping;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<User, GetUserDto>()
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.UserRoles.Select(ur => ur.Role)));
        CreateMap<CreateUserDto, User>()
            .ForMember(dest => dest.PasswordHash,
                opt => opt.MapFrom(src => BCrypt.Net.BCrypt.HashPassword(src.Password)));
        CreateMap<Ticket, GetTicketDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        CreateMap<CreateTicketDto, Ticket>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => TicketStatus.Abierto));
        CreateMap<Role, GetRoleDto>();
        CreateMap<CreateRoleDto, Role>();
        CreateMap<Response, GetResponseDto>();
        CreateMap<CreateResponseDto, Response>();
    }
}