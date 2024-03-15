using SibSIU.Core.MailService.Mailer.Base;

namespace SibSIU.Domain.SendMail.MailModels;
public sealed class MailPhoneChangeModel(string newPhone, Dictionary<string, InLineAttachment> inLineAttachments)
    : BaseMailModel(inLineAttachments)
{
    public string NewPhone => newPhone;
}
