namespace SibSIU.Identity.Models.Posts;
public sealed class PostItem
{
    public Ulid Id { get; set; }
    public string Name { get; set; }

    public PostItem(Ulid id, string name)
    {
        Id = id;
        Name = name;
    }

    public PostItem() : this(Ulid.Empty, string.Empty) { }

    public override bool Equals(object? obj)
    {
        return obj is PostItem other && other.Id == this.Id;
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
