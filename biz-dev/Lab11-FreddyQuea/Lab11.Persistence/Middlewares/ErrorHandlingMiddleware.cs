using System.Net;
using System.Text.Json;

namespace Lab11.Persistence.Middlewares;

public class ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);

            // Manejar respuestas de no autorización (401/403) después de ejecutar el pipeline
            if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
            {
                await HandleUnauthorizedAsync(context);
            }
            else if (context.Response.StatusCode == (int)HttpStatusCode.Forbidden)
            {
                await HandleForbiddenAsync(context);
            }
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleUnauthorizedAsync(HttpContext context)
    {
        logger.LogWarning("Acceso no autorizado: {Method} {Path}", context.Request.Method, context.Request.Path);
        return WriteErrorResponseAsync(context, HttpStatusCode.Unauthorized,
            "No estás autorizado para acceder a este recurso");
    }

    private Task HandleForbiddenAsync(HttpContext context)
    {
        logger.LogWarning("Acceso prohibido: {Method} {Path}", context.Request.Method, context.Request.Path);
        return WriteErrorResponseAsync(context, HttpStatusCode.Forbidden,
            "No tienes permisos suficientes para realizar esta acción");
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        // Registrar el error completo en los logs
        logger.LogError(ex, "Error no controlado en {Method} {Path}: {Message}",
            context.Request.Method, context.Request.Path, ex.Message);

        // Determinar el código de estado y mensaje según el tipo de excepción
        var (statusCode, message) = ex switch
        {
            ArgumentException _ => (HttpStatusCode.BadRequest, ex.Message),
            KeyNotFoundException _ => (HttpStatusCode.NotFound, ex.Message),
            UnauthorizedAccessException _ => (HttpStatusCode.Unauthorized, ex.Message),
            { } e when e.Message == "Invalid credentials" => (HttpStatusCode.Unauthorized,
                "Credenciales inválidas"),
            { } e => (HttpStatusCode.BadRequest, e.Message) // Mostrar mensaje de cualquier Exception
        };

        await WriteErrorResponseAsync(context, statusCode, message);
    }

    private async Task WriteErrorResponseAsync(HttpContext context, HttpStatusCode statusCode, string message)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var errorResponse = new
        {
            error = message,
            status = (int)statusCode
        };

        var jsonResponse = JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        await context.Response.WriteAsync(jsonResponse);
    }
}

public static class ErrorHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ErrorHandlingMiddleware>();
    }
}