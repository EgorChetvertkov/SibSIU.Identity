namespace SibSIU.Identity.Models.AcademicGroups;
public sealed class AcademicGroupItem
{
    public string GroupName { get; set; }

    public AcademicGroupItem(string groupName)
    {
        GroupName = groupName?.Trim() ?? string.Empty;
    }

    public AcademicGroupItem() : this(string.Empty) { }

    public override bool Equals(object? obj)
    {
        return obj is AcademicGroupItem other && other.GroupName == this.GroupName;
    }

    public override int GetHashCode()
    {
        return GroupName.GetHashCode();
    }

    public override string ToString()
    {
        return GroupName;
    }
}
