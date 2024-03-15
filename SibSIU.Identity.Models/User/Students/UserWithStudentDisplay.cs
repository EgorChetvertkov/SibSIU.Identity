namespace SibSIU.Identity.Models.User.Students;
public sealed class UserWithStudentDisplay
{
    public Ulid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
    public DateTimeOffset BirthOfDate { get; set; }
    public string GenderName { get; set; }
    public List<StudentInfo> Students { get; set; }

    public UserWithStudentDisplay(
        Ulid id,
        string userName,
        string email,
        string phoneNumber,
        string firstName,
        string lastName,
        string patronymic,
        DateTimeOffset birthOfDate,
        string? genderName,
        List<StudentInfo> students)
    {
        Id = id;
        UserName = userName;
        Email = email;
        PhoneNumber = phoneNumber;
        FirstName = firstName;
        LastName = lastName;
        Patronymic = patronymic;
        BirthOfDate = birthOfDate;
        GenderName = genderName ?? string.Empty;
        Students = students;
    }

    public UserWithStudentDisplay()
        : this(
              Ulid.Empty,
              string.Empty,
              string.Empty,
              string.Empty,
              string.Empty,
              string.Empty,
              string.Empty,
              DateTimeOffset.MinValue,
              string.Empty,
              [])
    { }

    public override bool Equals(object? obj)
    {
        return obj is UserWithStudentDisplay other && other.Id == Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public override string ToString()
    {
        return $"{LastName} {FirstName} {Patronymic ?? string.Empty}".Trim();
    }
}
