using SibSIU.Core.Authenticate.Password;
using SibSIU.Identity.Models.User.Students;
using SibSIU.UserData.Database.Entities;

namespace SibSIU.Domain.Dean.Synchronization.Commands.ImportingStudentsFromDean.Models;
public sealed class PlainStudentData
{
    public Ulid Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string? Patronymic { get; init; }
    public string UserName { get; init; }
    public string PlainPassword { get; init; }
    public string EmailAddress { get; init; }
    public DateTimeOffset Birthday { get; init; }
    public int DeanCode { get; init; }
    public double Rank { get; init; }
    public AcademicGroup AcademicGroup { get; init; }

    public PlainStudentData(
        Ulid id,
        string firstName,
        string lastName,
        string? patronymic,
        string userName,
        string plainPassword,
        string emailAddress,
        DateTimeOffset birthday,
        int deanCode,
        double rank,
        AcademicGroup academicGroup)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Patronymic = patronymic;
        UserName = userName;
        PlainPassword = plainPassword;
        EmailAddress = emailAddress;
        Birthday = birthday;
        DeanCode = deanCode;
        Rank = rank;
        AcademicGroup = academicGroup;
    }

    internal CreateStudent GetCreateStudent()
    {
        var hashPassword = HashCalculator.Hash(PlainPassword);
        return new(
            Id,
            FirstName,
            LastName,
            Patronymic,
            UserName,
            hashPassword.Password,
            hashPassword.Salt,
            EmailAddress,
            Birthday,
            DeanCode,
            Rank,
            AcademicGroup);
    }

    internal CsvStudentListItem GetCsvStudentListItem()
    {
        return new(this);
    }

    internal UserWithStudentDisplay GetUserWithStudentDisplay()
    {
        List<StudentInfo> studentInfos = [];

        studentInfos.Add(new(DeanCode, AcademicGroup.Name, Rank));

        return new(
            Id,
            UserName,
            EmailAddress,
            string.Empty,
            FirstName,
            LastName,
            Patronymic ?? string.Empty,
            Birthday,
            string.Empty,
            studentInfos);
    }
}
