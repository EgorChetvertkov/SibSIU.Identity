namespace SibSIU.UserData.Database.Entities;
public sealed class TempStudentDeanCodeFromDeanDB(int deanCode, string firstName, string lastName, string? patronymic, string groupName, double rank, DateTimeOffset birthday)
{
    public int DeanCode { get; set; } = deanCode;
    public string FirstName { get; set; } = firstName;
    public string LastName { get; set; } = lastName;
    public string? Patronymic { get; set; } = patronymic;
    public string GroupName { get; set; } = groupName;
    public double Rank { get; set; } = rank;
    public DateTimeOffset Birthday { get; set; } = birthday.ToUniversalTime();

    public override bool Equals(object? obj)
    {
        return obj is TempStudentDeanCodeFromDeanDB other
            && other.DeanCode == this.DeanCode;
    }

    public override int GetHashCode()
    {
        return DeanCode.GetHashCode();
    }
}