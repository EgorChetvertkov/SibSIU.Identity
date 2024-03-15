namespace SibSIU.Domain.Dean.Synchronization.Commands.SynchronizationWithDean.Models;
public sealed class SelectDeanInstitutes(int deanCode, string fullName, string shortName)
{
    public int DeanCode { get; init; } = deanCode;
    public string FullName { get; init; } = fullName;
    public string ShortName { get; init; } = shortName;

    public SelectDeanInstitutes() : this(-1, string.Empty, string.Empty) { }

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
            obj is SelectDeanInstitutes other &&
            other.DeanCode == DeanCode;
    }
}
