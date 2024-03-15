namespace SibSIU.Identity.Models.User.Manage;
public sealed class UserItem
{
    public Ulid Id { get; set; }
    public string FullName { get; set; }

    public UserItem(Ulid id, string firstName, string lastName, string? patronymic)
    {
        Id = id;
        FullName = $"{lastName} {firstName} {patronymic ?? string.Empty}".Trim();
    }

    public UserItem() : this(Ulid.Empty, string.Empty, string.Empty, string.Empty) { }

    public override bool Equals(object? obj)
    {
        return obj is UserItem other && other.Id == this.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public override string ToString()
    {
        return FullName;
    }
}
