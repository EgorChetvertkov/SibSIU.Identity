namespace SibSIU.Identity.Models.User.Manage;
public sealed class UserRowItem
{
    public Ulid Id { get; set; }
    public string UserName { get; set; }
    public string FirstName { internal get; set; }
    public string LastName { internal get; set; }
    public string Patronymic { internal get; set; }
    public string FullName { get; }

    public UserRowItem(Ulid id, string userName, string firstName, string lastName, string? patronymic)
    {
        Id = id;
        UserName = userName;
        FirstName = firstName;
        LastName = lastName;
        Patronymic = patronymic ?? string.Empty;
        FullName = $"{LastName} {FirstName} {Patronymic}".Trim();
    }

    public UserRowItem() : this(Ulid.Empty, string.Empty, string.Empty, string.Empty, string.Empty) { }
}
