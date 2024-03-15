namespace SibSIU.Identity.Models.Posts;
public sealed class PostRowItem
{
    public Ulid Id { get; }
    public string Name { get; }

    public PostRowItem(Ulid id, string name)
    {
        Id = id;
        Name = name;
    }

    public PostRowItem() : this(Ulid.Empty, string.Empty) { }
}
