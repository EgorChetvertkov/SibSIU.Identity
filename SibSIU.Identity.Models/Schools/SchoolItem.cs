namespace SibSIU.Identity.Models.Schools;
public sealed class SchoolItem
{
    public Ulid Id { get; set; }
    public string Name { get; set; }

    public SchoolItem(Ulid id, string fullName, string shortName)
    {
        Id = id;
        Name = $"{fullName} ({shortName})";
    }

    public SchoolItem() : this(Ulid.Empty, string.Empty, string.Empty) { }

    public override bool Equals(object? obj)
    {
        return obj is SchoolItem other && other.Id == this.Id;
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
