using Microsoft.EntityFrameworkCore;

namespace SibSIU.Identity.Database;
public sealed class OpenIdConnectContext(DbContextOptions<OpenIdConnectContext> options)
    : DbContext(options)
{
}
