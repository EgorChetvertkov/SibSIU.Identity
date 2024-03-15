namespace SibSIU.Identity.Models.Units;
public sealed class UnitRowItem
{
    public Ulid Id { get; }
    public string FullName { get; }
    public string ShortName { get; }
    public List<UnitRowItem> Children { get; }

    public UnitRowItem(Ulid id, string fullName, string shortName, List<UnitRowItem> children)
    {
        Id = id;
        FullName = fullName;
        ShortName = shortName;
        Children = children;
    }

    public UnitRowItem() : this(Ulid.Empty, string.Empty, string.Empty, []) { }
}
