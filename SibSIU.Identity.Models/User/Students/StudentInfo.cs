namespace SibSIU.Identity.Models.User.Students;
public sealed class StudentInfo
{
    public int StudentDeanCode { get; set; }
    public string GroupName { get; set; }
    public double Rank { get; set; }

    public StudentInfo(int studentDeanCode, string groupName, double rank)
    {
        StudentDeanCode = studentDeanCode;
        GroupName = groupName;
        Rank = rank;
    }

    public StudentInfo() : this(0, string.Empty, 0) { }

    public override bool Equals(object? obj)
    {
        return obj is StudentInfo other && other.StudentDeanCode == StudentDeanCode;
    }

    public override int GetHashCode()
    {
        return StudentDeanCode;
    }

    public override string ToString()
    {
        return $"обучающийся группы {GroupName}";
    }
}
