using SibSIU.UserData.Database.Entities;

namespace SibSIU.Domain.Dean.Synchronization.Commands.ImportingStudentsFromDean.Models;

public sealed class CreateStudent
{
    public Ulid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Patronymic { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string PasswordSalt { get; set; }
    public string EmailAddress { get; set; }
    public DateTimeOffset? Birthday { get; set; }
    public int DeanCode { get; set; }
    public double Rank { get; set; }
    public AcademicGroup AcademicGroup { get; set; }

    public CreateStudent(
        Ulid id,
        string firstName,
        string lastName,
        string? patronymic,
        string userName,
        string password,
        string passwordSalt,
        string emailAddress,
        DateTimeOffset? birthday,
        int deanCode,
        double rank,
        AcademicGroup academicGroup)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Patronymic = patronymic;
        UserName = userName;
        Password = password;
        PasswordSalt = passwordSalt;
        EmailAddress = emailAddress;
        Birthday = birthday;
        DeanCode = deanCode;
        Rank = rank;
        AcademicGroup = academicGroup;
    }
}