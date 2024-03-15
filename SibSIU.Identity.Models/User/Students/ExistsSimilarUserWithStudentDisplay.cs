namespace SibSIU.Identity.Models.User.Students;
public sealed class ExistsSimilarUserWithStudentDisplay
{
    public UserWithStudentDisplay User { get; set; }
    public bool MastCombine { get; set; }

    public ExistsSimilarUserWithStudentDisplay(UserWithStudentDisplay user, bool mastCombine)
    {
        User = user;
        MastCombine = mastCombine;
    }

    public ExistsSimilarUserWithStudentDisplay() : this(new(), false) { }
}
