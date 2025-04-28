namespace LAB06_FreddyQuea.Utilities;

public record class ServiceResponse<T>(bool success, string message, T? data = default)
{
}