using SibSIU.Core.Services.ResultObject;

namespace SibSIU.Domain.UserManager.Errors;
public static class SchoolErrors
{
    public static Error InvalidSchoolId =>
        Error.Validation("Идентификатор школы указан не корректно");
    public static Error InvalidClassNumber =>
        Error.Validation("Класс должен быть от 1 до 11");
    public static Error InvalidClassLitter =>
        Error.Validation("Параллель должна быть задана буквой");
    public static Error SchoolNotFound =>
        Error.NotFound("Школу не удалось обнаружить");
    public static Error SchoolFullNameEmpty =>
        Error.Validation("Полное наименование школы должно быть указано");
    public static Error SchoolShortNameEmpty =>
        Error.Validation("Сокращенное наименование школы должно быть указано");
    public static Error SchoolFullNameAlreadyExists =>
        Error.Conflict("Полное наименование школы уже используется для другого учреждения");
    public static Error SchoolShortNameAlreadyExists =>
        Error.Conflict("Сокращенное наименование школы уже используется для другого учреждения");
    public static Error SchoolUse =>
        Error.BadRequest("Школа не существует или на данный момент к ней прикреплен хотя бы один школьник");
}
