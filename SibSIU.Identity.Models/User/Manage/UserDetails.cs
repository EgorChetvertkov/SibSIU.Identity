using SibSIU.Identity.Models.Genders;

namespace SibSIU.Identity.Models.User.Manage;
public sealed class UserDetails
{
    public Ulid Id { get; set; } 
    public string UserName { get; set; }
    public string Email { get; set; }
    public bool EmailConfirmed { get; set; }
    public string PhoneNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
    public DateTimeOffset BirthOfDate { get; set; }
    public GenderItem Gender { get; set; }

    public List<PartnerDetails> Partners { get; }
    public List<PupilDetails> Pupils { get; }
    public List<WorkDetails> Works { get; }
    public List<StudentDetails> Students {  get; }
    public List<ClaimDetails> Claims { get; }
    public List<RoleDetails> Roles { get; }

    public UserDetails(
        Ulid id, string userName, string email, bool emailConfirmed, string phoneNumber, string firstName,
        string lastName, string patronymic, DateTimeOffset birthOfDate, GenderItem gender, List<PartnerDetails> partner,
        List<PupilDetails> pupil, List<WorkDetails> works, List<StudentDetails> students, List<ClaimDetails> claims,
        List<RoleDetails> roles)
    {
        Id = id;
        UserName = userName;
        Email = email;
        EmailConfirmed = emailConfirmed;
        PhoneNumber = phoneNumber;
        FirstName = firstName;
        LastName = lastName;
        Patronymic = patronymic;
        BirthOfDate = birthOfDate;
        Gender = gender;
        Partners = partner;
        Pupils = pupil;
        Works = works;
        Students = students;
        Claims = claims;
        Roles = roles;
    }

    public UserDetails() : this(
        Ulid.Empty, string.Empty, string.Empty, false,
        string.Empty, string.Empty, string.Empty, string.Empty,
        DateTimeOffset.MinValue, new(),
        [], [], [], [], [], [])
    { }
}
