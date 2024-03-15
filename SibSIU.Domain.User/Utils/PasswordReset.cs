using Microsoft.AspNetCore.WebUtilities;

using System.Security.Cryptography;
using System.Text;

namespace SibSIU.Domain.UserManager.Utils;
public static class PasswordReset
{
    public static string Hash(string password, string userId) =>
        WebEncoders.Base64UrlEncode(
            Encoding.UTF8.GetBytes(
                HashWithoutEncode(password, userId)));

    private static string HashWithoutEncode(string password, string userId) =>
        BitConverter.ToString(
            SHA256.HashData(
                Encoding.UTF8.GetBytes($"{password}_{userId}")))
        .Replace("-", string.Empty);

    public static bool IsEqualCode(string code, string password, string userId) =>
        Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code)) == HashWithoutEncode(password, userId);
}
