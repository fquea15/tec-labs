namespace Server.Utils;

public class ServiceResponse(bool success , string message)
{
    public bool Success { get; set; } = success;
    public string Message { get; set; } = message;
}