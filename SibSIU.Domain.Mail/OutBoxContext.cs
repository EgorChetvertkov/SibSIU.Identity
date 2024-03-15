using Microsoft.EntityFrameworkCore;

using SibSIU.Core.MailService.OutboxStorage;

namespace SibSIU.Domain.SendMail;
public sealed class OutBoxContext(DbContextOptions<OutBoxContext> options) : AbstractOutBoxContext(options)
{
}
