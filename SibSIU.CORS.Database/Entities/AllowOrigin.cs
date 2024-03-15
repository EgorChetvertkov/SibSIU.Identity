using SibSIU.Core.Database.EF.Entities;

namespace SibSIU.CORS.Database.Entities;

public class AllowOrigin : EntityWithUlidId
{
    public required string Creator { get; set; }
    public required string Origin { get; set; }
}
