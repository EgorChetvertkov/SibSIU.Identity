namespace SibSIU.Identity.Models.User.Manage;
public sealed class UnconfirmedUserDetails
{
    public Ulid Id { get; set; }
    public string UserName { get; set; }
    public string FirstName { internal get; set; }
    public string LastName { internal get; set; }
    public string Patronymic { internal get; set; }
    public string FullName { get; }
    public PupilDetails? Pupil { get; set; }
    public StudentDetails? Student { get; set; }
    public PartnerDetails? Partner { get; set; }

    public UnconfirmedUserDetails(
        Ulid id,
        string userName,
        string firstName,
        string lastName,
        string patronymic,
        PupilDetails? pupil,
        StudentDetails? student,
        PartnerDetails? partner)
    {
        Id = id;
        UserName = userName;
        FirstName = firstName;
        LastName = lastName;
        Patronymic = patronymic;
        FullName = $"{LastName} {FirstName} {Patronymic ?? string.Empty}".Trim();
        Pupil = pupil;
        Student = student;
        Partner = partner;
    }

    public UnconfirmedUserDetails() : this(
        Ulid.Empty,
        string.Empty,
        string.Empty,
        string.Empty,
        string.Empty,
        null,
        null,
        null) { }
}
