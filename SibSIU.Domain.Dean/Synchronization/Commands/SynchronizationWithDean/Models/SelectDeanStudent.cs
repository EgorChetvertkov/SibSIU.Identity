namespace SibSIU.Domain.Dean.Synchronization.Commands.SynchronizationWithDean.Models;
internal sealed class SelectDeanStudent(int deanCode, string firstName, string lastName, string? patronymic, string groupName, DateTime? birthday, double rank)
{
    public int DeanCode { get; set; } = deanCode;
    public string FirstName { get; set; } = firstName;
    public string LastName { get; set; } = lastName;
    public string? Patronymic { get; set; } = patronymic;
    public string GroupName { get; set; } = groupName;
    public DateTime? Birthday { get; set; } = birthday;
    public double Rank { get; set; } = rank;

    public override string ToString()
    {
        return $"{LastName} {FirstName} {Patronymic ?? string.Empty}".Trim();
    }

    public override bool Equals(object? obj)
    {
        return
            obj is not null &&
            obj is SelectDeanStudent other &&
            other.DeanCode == DeanCode;
    }

    public override int GetHashCode()
    {
        return DeanCode.GetHashCode();
    }
}
