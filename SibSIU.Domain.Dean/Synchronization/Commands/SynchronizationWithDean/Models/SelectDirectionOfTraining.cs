namespace SibSIU.Domain.Dean.Synchronization.Commands.SynchronizationWithDean.Models;
internal sealed class SelectDirectionOfTraining(int deanCode, string? name, string? code, int? departmentId, int? instituteId)
{
    public int DeanCode { get; set; } = deanCode;
    public string Name { get; set; } = name ?? string.Empty;
    public string Code { get; set; } = code ?? string.Empty;
    public int DepartmentDeanCode { get; set; } = departmentId ?? -1;
    public int InstituteDeanCode { get; set; } = instituteId ?? -1;

    public SelectDirectionOfTraining() : this(-1, string.Empty, string.Empty, -1, -1) { }

    public override string ToString()
    {
        return $"{Code} {Name}";
    }

    public override bool Equals(object? obj)
    {
        return
            obj is not null &&
            obj is SelectDirectionOfTraining other
            && other.DeanCode == DeanCode;
    }

    public override int GetHashCode()
    {
        return DeanCode.GetHashCode();
    }
}
