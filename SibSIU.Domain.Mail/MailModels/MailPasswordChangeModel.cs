using SibSIU.Core.MailService.Mailer.Base;

namespace SibSIU.Domain.SendMail.MailModels;
public sealed class MailPasswordChangeModel(string newPassword, Dictionary<string, InLineAttachment> inLineAttachments)
    : BaseMailModel(inLineAttachments)
{
    public string NewPassword => newPassword;
}
