using SibSIU.Core.Database.EF.Entities;

namespace SibSIU.UserData.Database.Entities;

public sealed class Organization : EntityWithUlidId
{
    public required string FullName { get; set; }
    public required string ShortName { get; set; }
    public required string OGRN { get; set; }
    public required string TIN { get; set; }
    public required string KPP { get; set; }

    public ICollection<Partner> Partners { get; set; } = [];
}