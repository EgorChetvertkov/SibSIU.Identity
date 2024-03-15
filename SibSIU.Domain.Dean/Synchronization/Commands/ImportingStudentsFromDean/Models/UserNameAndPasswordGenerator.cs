using Iuliia;

using System.Security.Cryptography;

namespace SibSIU.Domain.Dean.Synchronization.Commands.ImportingStudentsFromDean.Models;
internal static class UserNameAndPasswordGenerator
{
    public static UserNameAndPassword ByFullName(string fullName, int countStartWith)
    {
        string userName = GenerateUserName(fullName, countStartWith);
        string password = GetRandomPassword(8);

        return UserNameAndPassword.CreateByUserName(userName, password);
    }

    public static UserNameAndPassword ByEmail(string email)
    {
        string password = GetRandomPassword(8);

        return UserNameAndPassword.CreateByEmail(email, password);
    }

    private static string GenerateUserName(string fullName, int countStartWith)
    {
        string translateName = IuliiaTranslator.Translate(fullName, Schemas.Telegram);
        if(countStartWith == 0)
        {
            return translateName;
        }

        return $"{translateName}_{countStartWith}";
    }

    private static string GetRandomPassword(int length)
    {
        byte[] rgb = new byte[length];
        RandomNumberGenerator.Fill(rgb);
        return Convert.ToBase64String(rgb);
    }
}
