using SibSIU.Identity.Models.User.Students;

namespace SibSIU.Domain.Dean.Synchronization.Commands.SaveStudents.Models;
internal sealed class AddAcademicGroup
{
    public Ulid ExistsUserId { get; set; }
    public Ulid NewUserId { get; set; }
    public List<StudentInfo> NewStudentData { get; set; }

    public AddAcademicGroup(Ulid existsUserId, Ulid newUserId, List<StudentInfo> newStudentData)
    {
        ExistsUserId = existsUserId;
        NewUserId = newUserId;
        NewStudentData = newStudentData;
    }

    public AddAcademicGroup() : this(Ulid.Empty, Ulid.Empty, []) { }
}
