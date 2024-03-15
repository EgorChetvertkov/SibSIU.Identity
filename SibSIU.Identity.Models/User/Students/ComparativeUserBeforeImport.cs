namespace SibSIU.Identity.Models.User.Students;
public sealed class ComparativeUserBeforeImport
{
    public UserWithStudentDisplay User { get; set; }
    public List<ExistsSimilarUserWithStudentDisplay> ExistsSimilar { get; set; }

    public ComparativeUserBeforeImport(UserWithStudentDisplay user, List<ExistsSimilarUserWithStudentDisplay> existsSimilar)
    {
        User = user;
        ExistsSimilar = existsSimilar;
    }

    public ComparativeUserBeforeImport() : this(new(), []) { }
}
