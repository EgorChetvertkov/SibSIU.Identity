using SibSIU.Core.MailService.Mailer.Base;

namespace SibSIU.Domain.SendMail.MailModels;
public sealed class MailConfirmedEmailModel(string url, Dictionary<string, InLineAttachment> inLineAttachments)
    : BaseMailModel(inLineAttachments)
{
    public string Url => url;
}
