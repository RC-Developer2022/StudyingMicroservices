using System.Security.Cryptography;
using System.Text;

namespace AccessApplication.Methods;

public class PasswordCryptography
{
    private readonly byte[] _secretKey;

    public PasswordCryptography(string secretKey)
    {
        _secretKey = Encoding.UTF8.GetBytes(secretKey);
    }

    public string HashPassword(string password)
    {
        var hmac = new HMACSHA256(_secretKey);
        var bytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        var hmac = new HMACSHA256(_secretKey);
        var bytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes) == hashedPassword;
    }
}
