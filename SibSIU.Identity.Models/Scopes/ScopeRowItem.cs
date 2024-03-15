namespace SibSIU.Identity.Models.Scopes;
public sealed class ScopeRowItem
{
    public Ulid Id { get; }
    public string Name { get; }

    public ScopeRowItem(Ulid id, string name)
    {
        Id = id;
        Name = name;
    }

    public ScopeRowItem() : this(Ulid.Empty, string.Empty) { }
}
