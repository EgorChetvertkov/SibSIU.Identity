namespace SibSIU.Identity.Models.User.Manage;
public sealed class UnconfirmedUserRowItem
{
    public Ulid Id { get; set; }
    public string UserName { get; set; }
    public string FirstName { internal get; set; }
    public string LastName { internal get; set; }
    public string Patronymic { internal get; set; }
    public string Type { get; set; }
    public string FullName { get; }

    public UnconfirmedUserRowItem(
        Ulid id,
        string userName,
        string firstName,
        string lastName,
        string? patronymic,
        int countPupils,
        int countStudents,
        int countPartners)
    {
        Id = id;
        UserName = userName;
        FirstName = firstName;
        LastName = lastName;
        Patronymic = patronymic ?? string.Empty;
        FullName = $"{LastName} {FirstName} {Patronymic}".Trim();
        if (countPupils == 1 && countStudents == 0 && countPartners == 0)
        {
            Type = "Школьник";
        }
        else if (countPupils == 0 && countStudents == 1 && countPartners == 0)
        {
            Type = "Обучающийся";
        }
        else if (countPupils == 0 && countStudents == 0 && countPartners == 1)
        {
            Type = "Партнер";
        }
        else
        {
            Type = "Не известно";
        }
    }

    public UnconfirmedUserRowItem() : this(
        Ulid.Empty,
        string.Empty,
        string.Empty,
        string.Empty,
        string.Empty,
        0, 0, 0) { }
}
