namespace SibSIU.Domain.Dean.Synchronization.Commands.SynchronizationWithDean.Models;
internal sealed class SelectDeanGroup(string name, int course, int level, int form, int dot, int institute)
{
    public string Name { get; set; } = name;
    public int Course { get; set; } = course;
    public int AcademicLevelDeanCode { get; set; } = level;
    public int AcademicFormDeanCode { get; set; } = form;
    public int DirectionOfTrainingDeanCode { get; set; } = dot;
    public int InstituteDeanCode { get; set; } = institute;

    public SelectDeanGroup() : this(string.Empty, -1, -1, -1, -1, -1) { }

    public override string ToString()
    {
        return Name;
    }

    public override bool Equals(object? obj)
    {
        return
            obj is not null &&
            obj is SelectDeanGroup other &&
            other.Name == Name;
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}
