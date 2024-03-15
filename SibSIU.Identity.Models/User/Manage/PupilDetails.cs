using SibSIU.Identity.Models.Schools;

namespace SibSIU.Identity.Models.User.Manage;

public sealed class PupilDetails
{
    public Ulid PupilId { get; set; }
    public SchoolItem School { get; set; }
    public char ClassLitter { get; set; }
    public int ClassNumber { get; set; }
    public string ClassName => $"{ClassLitter}{ClassNumber}";

    public PupilDetails(Ulid pupilId, SchoolItem school, char classLitter, int classNumber)
    {
        PupilId = pupilId;
        School = school;
        ClassLitter = classLitter;
        ClassNumber = classNumber;
    }

    public PupilDetails() : this(Ulid.Empty, new(), char.MinValue, 0) { }
}