using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;

using SibSIU.Core.MailService.Mailer;
using SibSIU.Core.MailService.Mailer.Base;
using SibSIU.Domain.SendMail.MailModels;
using SibSIU.Identity.Web.Utils;

using System.Security.Cryptography;
using System.Text;

namespace SibSIU.Domain.UserManager.Utils;
public static class EmailConfirmed
{
    public static string Hash(string email, string userId) =>
        WebEncoders.Base64UrlEncode(
            Encoding.UTF8.GetBytes(
                HashWithoutEncode(email, userId)));

    private static string HashWithoutEncode(string email, string userId) =>
        BitConverter.ToString(
            SHA256.HashData(
                Encoding.UTF8.GetBytes($"{email}_{userId}")))
        .Replace("-", string.Empty);

    public static bool IsEqualCode(string code, string email, string userId) =>
        Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code)) == HashWithoutEncode(email, userId);

    public static async Task SendEmailConfirmationMail(
        IEmailSender emailService,
        MailPathForUserManager path,
        NavigationManager manager,
        string email,
        string userId,
        CancellationToken cancellationToken)
    {
        string code = EmailConfirmed.Hash(email, userId);
        var callbackUrl = manager.GetUriWithQueryParameters(
        manager.ToAbsoluteUri("confirmed_email").AbsoluteUri,
            new Dictionary<string, object?> { ["userId"] = userId, ["code"] = code });

        Dictionary<string, InLineAttachment> inLine = [];
        inLine.Add("logo", new("Logo_image", path.LogoPath));

        await emailService.SendMailWithTemplate(
            new MailMessageWithTemplate<MailConfirmedEmailModel>(
                "Подтверждение электронной почты",
                email, path.EmailConfirmedTemplatePath,
                new MailConfirmedEmailModel(callbackUrl, inLine)),
            [],
            cancellationToken);
    }
}
