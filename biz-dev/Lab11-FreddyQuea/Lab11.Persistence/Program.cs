using Lab11.Persistence.Configuration;

namespace Lab11.Persistence;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddPersistenceService(builder.Configuration);
        var app = builder.Build();
        app.ConfigurePipeline();
        app.Run();
    }
}