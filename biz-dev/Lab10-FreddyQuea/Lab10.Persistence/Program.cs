using Lab10.Persistence.Configuration;
using Lab10.Persistence.Middleware;

namespace Lab10.Persistence;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.ConfigureServices(builder.Configuration);
        var app = builder.Build();
        app.UseErrorHandling();
        app.ConfigurePipeline();
        app.Run();
    }
}