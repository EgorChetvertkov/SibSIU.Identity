using System.Text.RegularExpressions;

namespace SibSIU.Domain.UserManager.Utils;
public partial class PhoneValidation
{
    [GeneratedRegex("^\\(?[+]?([0-9]{0,}?)[-. ]?[(]?([0-9]{3})\\)?[)]?[-. ]?([0-9]{3})[-. ]?([0-9]{2})[-. ]?([0-9]{2})$")]
    private static partial Regex ValidatePhone();
    [GeneratedRegex(@"[^\d]")]
    private static partial Regex OnlyNotDigit();

    public static bool Validate(string phone) => ValidatePhone().IsMatch(phone);
    public static string ReturnOnlyDigits(string phone) => OnlyNotDigit().Replace(phone, string.Empty);
}
