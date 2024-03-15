using SibSIU.Identity.Models.AcademicGroups;

namespace SibSIU.Identity.Models.User.Manage;

public sealed class StudentDetails
{
    public Ulid StudentId { get; set; }
    public AcademicGroupItem Group { get; set; }

    public StudentDetails(Ulid studentId, AcademicGroupItem group)
    {
        StudentId = studentId;
        Group = group;
    }

    public StudentDetails() : this(Ulid.Empty, new()) { }
}