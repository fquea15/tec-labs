using Lab10.Application.Interfaces;
using Lab10.Application.Mapping;
using Lab10.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Lab10.Application.Configuration;

public static class ApplicationServicesExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // AutoMapper
        services.AddAutoMapper(typeof(MappingConfig));

        // Services
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITicketService, TicketService>();
        services.AddScoped<IResponseService, ResponseService>();
        services.AddScoped<IRoleService, RoleService>();

        return services;
    }
}