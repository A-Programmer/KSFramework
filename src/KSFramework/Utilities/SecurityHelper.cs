using System.Security.Cryptography;
using System.Text;

namespace KSFramework.Utilities;
public static class SecurityHelper
{
    public static string GetSha256Hash(string input)
    {
        //using (var sha256 = new SHA256CryptoServiceProvider())
        var byteValue = Encoding.UTF8.GetBytes(input);
        var byteHash = SHA256.HashData(byteValue);
        return Convert.ToBase64String(byteHash);
        //return BitConverter.ToString(byteHash).Replace("-", "").ToLower();
    }

    public static string GenerateToken(int byteLength = 32)
    {
        Span<byte> randomNumber = stackalloc byte[byteLength];
        RandomNumberGenerator.Fill(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}