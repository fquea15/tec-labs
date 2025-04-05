using Lab03_FreddyQuea.Data;
using Lab03_FreddyQuea.Mapping;
using Lab03_FreddyQuea.Models;
using Lab03_FreddyQuea.Repositories.Implementations;
using Lab03_FreddyQuea.Repositories.Interfaces;
using Lab03_FreddyQuea.Services.Implementations;
using Lab03_FreddyQuea.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lab03_FreddyQuea.DependencyInjection;

public static class ServiceContainer
{
    public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration)
    {
        // (Database & Repositories)
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                npgsqlOptions =>
                {
                    npgsqlOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                }));

        services.AddScoped<IGenericRepository<User>, GenericRepository<User>>();
        services.AddScoped<IGenericRepository<Models.Task>, GenericRepository<Models.Task>>();

        // (AutoMapper & Services)
        services.AddAutoMapper(typeof(MappingConfig));
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITaskService, TaskService>();

        return services;
    }
}
