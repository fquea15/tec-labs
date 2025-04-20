namespace Server.Utils;

public record class ServiceResponse<T>(bool success, string message, T? data = default)
{
}
