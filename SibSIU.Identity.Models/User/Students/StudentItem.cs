namespace SibSIU.Identity.Models.User.Students;
public sealed class StudentItem
{
    public int DeanCode { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Patronymic { get; set; }
    public string FullName { get; }

    public StudentItem(int deanCode, string firstName, string lastName, string? patronymic)
    {
        DeanCode = deanCode;
        FirstName = firstName;
        LastName = lastName;
        Patronymic = patronymic;
        FullName = $"{LastName} {FirstName} {Patronymic ?? string.Empty}".Trim();
    }

    public StudentItem() : this(0, string.Empty, string.Empty, string.Empty) { }

    public override bool Equals(object? obj)
    {
        return obj is StudentItem other && other.DeanCode == this.DeanCode;
    }

    public override int GetHashCode()
    {
        return DeanCode.GetHashCode();
    }

    public override string ToString()
    {
        return FullName;
    }
}
