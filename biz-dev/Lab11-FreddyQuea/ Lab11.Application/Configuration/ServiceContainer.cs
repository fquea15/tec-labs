using Lab11.Application.Interfaces;
using Lab11.Application.Mappings;
using Lab11.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Lab11.Application.Configuration;

public static class ServiceContainer
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceContainer).Assembly));
        
        // Services
        services.AddScoped<IAuthService, AuthService>();
        return services;
    }
}