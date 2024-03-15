using Microsoft.EntityFrameworkCore;

using SibSIU.Core.Database.EF.Contexts;
using SibSIU.CORS.Database.Entities;

namespace SibSIU.CORS.Database;
public sealed class CORSContext(DbContextOptions<CORSContext> options)
    : BaseTransactionContext(options)
{
    public DbSet<AllowOrigin> Origins { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(CORSContext).Assembly);
    }
}
