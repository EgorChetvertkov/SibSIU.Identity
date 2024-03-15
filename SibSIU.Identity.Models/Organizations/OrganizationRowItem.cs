namespace SibSIU.Identity.Models.Organizations;
public sealed class OrganizationRowItem
{
    public Ulid Id { get; set; }
    public string FullName { get; set; }
    public string ShortName { get; set; }

    public OrganizationRowItem(Ulid id, string fullName, string shortName)
    {
        Id = id;
        FullName = fullName;
        ShortName = shortName;
    }

    public OrganizationRowItem() : this(Ulid.Empty, string.Empty, string.Empty) { }
}
