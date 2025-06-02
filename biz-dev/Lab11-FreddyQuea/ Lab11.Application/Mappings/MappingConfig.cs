using AutoMapper;
using Lab11.Application.DTOs.Response;
using Lab11.Application.DTOs.Role;
using Lab11.Application.DTOs.Ticket;
using Lab11.Application.DTOs.User;
using Lab11.Application.UseCases.Responses.Commands;
using Lab11.Application.UseCases.Roles.Commands;
using Lab11.Application.UseCases.Tickets.Commands;
using Lab11.Application.UseCases.Users.Commands;
using Lab11.Domain.Entities;
using Lab11.Domain.Enums;

namespace Lab11.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Users
            CreateMap<CreateUserCommand, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => BCrypt.Net.BCrypt.HashPassword(src.Password)))
                .ForMember(dest => dest.UserId, opt => opt.Ignore());
            CreateMap<User, GetUserDto>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.UserRoles.Select(ur => ur.Role)));

            // Roles
            CreateMap<CreateRoleCommand, Role>()
                .ForMember(dest => dest.RoleId, opt => opt.Ignore());
            CreateMap<Role, GetRoleDto>();
            
            // Tickets
            CreateMap<CreateTicketCommand, Ticket>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => TicketStatus.Abierto))
                .ForMember(dest => dest.TicketId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore());
            CreateMap<Ticket, GetTicketDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
            
            // Responses
            CreateMap<CreateResponseCommand, Response>()
                .ForMember(dest => dest.ResponseId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
            CreateMap<Response, GetResponseDto>();
        }
    }
}