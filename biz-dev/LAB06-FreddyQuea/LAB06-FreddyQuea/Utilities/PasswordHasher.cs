using System.Security.Cryptography;
using LAB06_FreddyQuea.Utilities.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace LAB06_FreddyQuea.Utilities;

public class PasswordHasher : IPasswordHasher
{
    private const int SaltSize = 16;
    private const int HashSize = 32;
    private const int Iterations = 10000;
    private const int MinPasswordLength = 8;

    public string HashPassword(string password)
    {
        if (string.IsNullOrEmpty(password) || password.Length < MinPasswordLength)
        {
            throw new ArgumentException("La contraseña debe tener al menos 8 caracteres.");
        }

        using var rng = RandomNumberGenerator.Create();
        var salt = new byte[SaltSize];
        rng.GetBytes(salt);

        var hash = KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA256, Iterations, HashSize);

        byte[] hashBytes = new byte[SaltSize + HashSize];
        Buffer.BlockCopy(salt, 0, hashBytes, 0, SaltSize);
        Buffer.BlockCopy(hash, 0, hashBytes, SaltSize, HashSize);

        return Convert.ToBase64String(hashBytes);
    }

    public bool VerifyPassword(string enteredPassword, string storedHash)
    {
        if (string.IsNullOrEmpty(storedHash))
        {
            throw new ArgumentException("El hash almacenado es nulo o vacío.");
        }

        var hashBytes = Convert.FromBase64String(storedHash);

        var salt = new byte[SaltSize];
        Buffer.BlockCopy(hashBytes, 0, salt, 0, SaltSize);

        var hash = KeyDerivation.Pbkdf2(enteredPassword, salt, KeyDerivationPrf.HMACSHA256, Iterations, HashSize);

        for (var i = 0; i < HashSize; i++)
        {
            if (hashBytes[SaltSize + i] != hash[i])
            {
                return false;
            }
        }

        return true;
    }
}