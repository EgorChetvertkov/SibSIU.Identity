using CsvHelper.Configuration.Attributes;

namespace SibSIU.Domain.Dean.Synchronization.Commands.ImportingStudentsFromDean.Models;
[Delimiter(",")]
public sealed class CsvStudentListItem
{
    [Name("ФИО")]
    public string FullName { get; init; }
    [Name("Логин")]
    public string Login { get; init; }
    [Name("Пароль")]
    public string Password { get; init; }
    [Name("Группа")]
    public string GroupName { get; init; }

    public CsvStudentListItem(PlainStudentData plainStudent)
    {
        FullName = $"{plainStudent.LastName} {plainStudent.FirstName} {plainStudent.Patronymic ?? ""}".Trim();
        Login = plainStudent.UserName;
        Password = plainStudent.PlainPassword;
        GroupName = plainStudent.AcademicGroup.Name;
    }
}
