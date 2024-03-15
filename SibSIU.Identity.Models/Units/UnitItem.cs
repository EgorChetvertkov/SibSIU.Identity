namespace SibSIU.Identity.Models.Units;
public sealed class UnitItem
{
    public Ulid Id { get; set; }
    public string Name { get; set; }

    public UnitItem(Ulid id, string fullName, string shortName)
    {
        Id = id;
        Name = $"{fullName} ({shortName})";
    }

    public UnitItem() : this(Ulid.Empty, string.Empty, string.Empty) { }

    public override bool Equals(object? obj)
    {
        return obj is UnitItem other && other.Id == this.Id;
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
