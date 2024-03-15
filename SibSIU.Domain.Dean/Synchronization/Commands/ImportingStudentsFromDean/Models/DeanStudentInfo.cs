using SibSIU.Auth.Database.Extensions;

namespace SibSIU.Domain.Dean.Synchronization.Commands.ImportingStudentsFromDean.Models;
internal sealed class DeanStudentInfo
{
    public int DeanCode { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string? Patronymic { get; init; }
    public string GroupName { get; init; }
    public double Rank { get; init; }
    public DateTimeOffset Birthday { get; init; }

    public string FullName => UserExtensions.GetFullName(FirstName, LastName, Patronymic);

    public DeanStudentInfo(string firstName, string lastName, string? patronymic, int deanCode, string groupName, DateTimeOffset birthday, double rank)
    {
        FirstName = firstName;
        LastName = lastName;
        Patronymic = patronymic;
        DeanCode = deanCode;
        Birthday = birthday;
        GroupName = groupName;
        Rank = rank;
    }
}
