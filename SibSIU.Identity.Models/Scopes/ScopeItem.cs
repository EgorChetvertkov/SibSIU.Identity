namespace SibSIU.Identity.Models.Scopes;
public sealed class ScopeItem
{
    public Ulid Id { get; }
    public string Name { get; } 

    public ScopeItem(Ulid id, string name)
    {
        Id = id;
        Name = name;
    }

    public ScopeItem() : this(Ulid.Empty, string.Empty) { }

    public override bool Equals(object? obj)
    {
        return obj is ScopeItem other && other.Id == Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public override string ToString()
    {
        return Name;
    }
}
