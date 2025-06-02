using Lab10.Persistence.Configuration;

namespace Lab10.Persistence;

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