using SibSIU.Core.Services.ResultObject;

namespace SibSIU.Domain.UserManager.Errors;
public static class StudentErrors
{
    public static Error InvalidGroupName =>
        Error.Validation("Невозможно идентифицировать группу");
    public static Error InvalidStudentDeanCode =>
        Error.Validation("Не обнаружено студента в системе Деканат");
    public static Error GroupNotFound =>
        Error.NotFound("Группа не обнаружена");
    public static Error InvalidStudentId =>
        Error.Validation("Невозможно идентифицировать обучающегося");
    public static Error StudentNotFound =>
        Error.NotFound("Студент не обнаружен");
    public static Error BirthdayMastBeEqualsWithDean =>
        Error.Conflict("Дата рождения должна совпадать с датой указанной в системе Деканат");
    public static Error DeanCodeAlreadyUse =>
        Error.BadRequest("Обучающийся уже имеет учетную запись");
}
