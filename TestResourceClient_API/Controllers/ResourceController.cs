using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TestResourceClient_API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ResourceController : ControllerBase
{
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetSecretResources()
    {
        await Task.Delay(1);
        var userClaims = HttpContext.User.Claims.Select(c => c.Value).ToList();
        return Ok(userClaims);
    }
}
