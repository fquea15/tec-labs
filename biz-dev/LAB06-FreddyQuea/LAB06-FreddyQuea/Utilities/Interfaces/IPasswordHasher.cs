namespace LAB06_FreddyQuea.Utilities.Interfaces;

public interface IPasswordHasher
{
    string HashPassword(string password);
    bool VerifyPassword(string enteredPassword, string storedHash);
}