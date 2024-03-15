namespace SibSIU.Domain.Dean.Synchronization.Commands.SynchronizationWithDean.Models;
internal sealed class SelectDeanForm(int deanCode, string fullName, string shortName)
{
    public int DeanCode { get; set; } = deanCode;
    public string FullName { get; set; } = fullName;
    public string ShortName { get; set; } = shortName;

    public SelectDeanForm() : this(-1, string.Empty, string.Empty) { }

    public override string ToString()
    {
        return $"{FullName}. {ShortName}";
    }

    public override int GetHashCode()
    {
        return DeanCode.GetHashCode();
    }

    public override bool Equals(object? obj)
    {
        return
            obj is not null &&
            obj is SelectDeanForm other &&
            other.DeanCode == DeanCode;
    }
}
