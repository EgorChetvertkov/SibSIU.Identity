namespace SibSIU.Identity.Models.User.Manage;
public sealed class RoleDetails
{
    public Ulid RoleId { get; set; }
    public string Name { get; set; }

    public RoleDetails(Ulid roleId, string name)
    {
        RoleId = roleId;
        Name = name;
    }

    public RoleDetails() : this(Ulid.Empty, string.Empty) { }
}
