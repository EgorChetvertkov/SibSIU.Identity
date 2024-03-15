namespace SibSIU.Identity.Models.Genders;
public sealed class GenderItem
{
    public Ulid Id { get; set; }
    public string Name { get; set; }

    public GenderItem(Ulid id, string name)
    {
        Id = id;
        Name = name;
    }

    public GenderItem() : this(Ulid.Empty, string.Empty) { }

    public override bool Equals(object? obj)
    {
        return obj is GenderItem other && other.Id == this.Id;
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
