namespace SibSIU.Domain.Dean.Synchronization.Commands.SynchronizationWithDean.Models;
internal sealed class SelectDeanDepartment(int deanCode, int instituteDeanCode, string fullName, string shortName)
{
    public int DeanCode { get; init; } = deanCode;
    public int InstituteDeanCode { get; init; } = instituteDeanCode;
    public string FullName { get; init; } = fullName;
    public string ShortName { get; init; } = shortName;

    public SelectDeanDepartment() : this(-1, -1, string.Empty, string.Empty) { }

    public override string ToString()
    {
        return $"{FullName} ({ShortName})";
    }

    public override int GetHashCode()
    {
        return DeanCode;
    }

    public override bool Equals(object? obj)
    {
        return
            obj is not null &&
            obj is SelectDeanDepartment other &&
            other.DeanCode == DeanCode;
    }
}
