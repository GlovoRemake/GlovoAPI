using System.Security.Cryptography;
using System.Text;
using Core.Interfaces;

namespace Core.Services;

public class HashService : IHashService
{
    public string Hash(string input)
    {
        using var sha = SHA256.Create();
        var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
        return Convert.ToBase64String(bytes);
    }
}