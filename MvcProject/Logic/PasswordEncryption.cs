using System.Security.Cryptography;
using System.Text;

namespace MvcProject.Logic;

// our own encryption class
public static class PasswordEncryption
{
    // hash password
    public static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var passwordBytes = Encoding.UTF8.GetBytes(password);
        var hashedBytes = sha256.ComputeHash(passwordBytes);
        return Convert.ToBase64String(hashedBytes);
    }
    
    // check password
    public static bool CheckPassword(string password, string hash) =>
        HashPassword(password) == hash;
}