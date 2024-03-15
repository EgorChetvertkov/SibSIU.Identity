using SibSIU.Core.Services.ResultObject;

namespace SibSIU.Domain.ExternalApplication.Errors;
public static class ApplicationErrors
{
    public static Error InvalidId =>
        Error.Validation("Идентификатор приложения не корректен");
    public static Error InvalidApplicationType =>
        Error.Validation("Тип приложения может быть только Web или Native");
    public static Error InvalidClientId =>
        Error.Validation("Идентификатор клиента не указан");
    public static Error InvalidClientType =>
        Error.Validation("Тип клиента может быть только Public или Confidential");
    public static Error InvalidClientSecret =>
        Error.Validation("Секрет должен быть указан");
    public static Error InvalidDisplayName =>
        Error.Validation("Имя приложения должно быть указано");
    public static Error NotFound =>
        Error.NotFound("Приложение не найдено");
}
