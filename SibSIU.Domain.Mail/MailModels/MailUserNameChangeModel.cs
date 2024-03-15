using SibSIU.Core.MailService.Mailer.Base;

namespace SibSIU.Domain.SendMail.MailModels;
public sealed class MailUserNameChangeModel(string newUserName, Dictionary<string, InLineAttachment> inLineAttachments)
    : BaseMailModel(inLineAttachments)
{
    public string NewUserName => newUserName;
}
