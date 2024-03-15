namespace SibSIU.Identity.Models.Schools;
public sealed class SchoolRowItem
{
    public Ulid Id { get; }
    public string FullName { get; }
    public string ShortName { get; }

    public SchoolRowItem(Ulid id, string fullName, string shortName)
    {
        Id = id;
        FullName = fullName;
        ShortName = shortName;
    }

    public SchoolRowItem() : this(Ulid.Empty, string.Empty, string.Empty) { }
}
