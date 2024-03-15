namespace SibSIU.Identity.Models.ClaimTypes;
public sealed class ClaimTypeItem
{
    public Ulid Id { get; }
    public string Name { get; }

    public ClaimTypeItem(Ulid id, string name)
    {
        Id = id;
        Name = name;
    }

    public ClaimTypeItem() : this(Ulid.Empty, string.Empty) { }

    public override bool Equals(object? obj)
    {
        return obj is ClaimTypeItem other && other.Id == Id;
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
