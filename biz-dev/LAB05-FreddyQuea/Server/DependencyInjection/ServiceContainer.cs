using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Mapping;
using Server.Repositories.Implementations;
using Server.Repositories.Interfaces;
using Server.Services.Implementations;
using Server.Services.Interfaces;

namespace Server.DependencyInjection;

public static class ServiceContainer
{
    public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Database configuration
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Default"),
                npgsqlOptions =>
                {
                    npgsqlOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                }));


        // AutoMapper
        services.AddAutoMapper(typeof(MappingConfig));

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Services
        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<ICourseService, CourseService>();
        services.AddScoped<ISubjectService, SubjectService>();
        services.AddScoped<IEnrollmentService, EnrollmentService>();

        return services;
    }
}