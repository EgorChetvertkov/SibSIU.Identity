namespace SibSIU.Identity.Models.Organizations;
public sealed class OrganizationItem
{
    public Ulid Id { get; set; }
    public string Name { get; set; }

    public OrganizationItem(Ulid id, string fullName, string shortName)
    {
        Id = id;
        Name = $"{fullName} ({shortName})".Trim();
    }

    public OrganizationItem() : this(Ulid.Empty, string.Empty, string.Empty) { }

    public override bool Equals(object? obj)
    {
        return obj is OrganizationItem other && other.Id == this.Id;
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
