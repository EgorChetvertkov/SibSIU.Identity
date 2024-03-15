namespace SibSIU.Identity.Models.CORSes;
public sealed class CORSItem
{
    public Ulid Id { get; set; }
    public string Origin { get; set; }

    public CORSItem(Ulid id, string origin)
    {
        Id = id;
        Origin = origin;
    }

    public CORSItem() : this(Ulid.Empty, string.Empty) { }

    public override bool Equals(object? obj)
    {
        return obj is CORSItem other && other.Id == this.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public override string ToString()
    {
        return Origin;
    }
}
