namespace SibSIU.Identity.Models.ClaimTypes;
public sealed class ClaimTypeRowItem
{
    public Ulid Id { get; }
    public string Name { get; }

    public ClaimTypeRowItem(Ulid id, string name)
    {
        Id = id;
        Name = name;
    }

    public ClaimTypeRowItem() : this(Ulid.Empty, string.Empty) { }
}
