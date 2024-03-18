using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SibSIU.Core.MailService.Mailer;
using SibSIU.Core.Names;
using SibSIU.Core.Services.ResultObject;
using System.Net.Mime;

namespace SibSIU.Identity.Controllers;
[Route("api/[controller]")]
[ApiController]
public class EmailController(IEmailSender sender) : ControllerBase
{
    [HttpPost("send_unsent")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Message), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
    [Authorize(Roles = RoleNames.BaseAdministrator)]
    public async Task<IActionResult> TrySendUnsent(CancellationToken cancellationToken)
    {
        await sender.TrySendAllUnsentMails(cancellationToken);
        return Ok(new Message("Письма отправлены"));
    }
}
