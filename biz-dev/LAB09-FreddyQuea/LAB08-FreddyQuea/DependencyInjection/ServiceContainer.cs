using Microsoft.EntityFrameworkCore;
using LAB08_FreddyQuea.Data;
using LAB08_FreddyQuea.Mapping;
using LAB08_FreddyQuea.Repositories.Implementations;
using LAB08_FreddyQuea.Repositories.Interfaces;
using LAB08_FreddyQuea.Services.Implementations;
using LAB08_FreddyQuea.Services.Interfaces;

namespace LAB08_FreddyQuea.DependencyInjection;


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
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<ICustumerRepository, CustomerRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Services
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IOrderService, OrderService>();

        return services;
    }
}