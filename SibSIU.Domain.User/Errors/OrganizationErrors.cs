using SibSIU.Core.Services.ResultObject;

namespace SibSIU.Domain.UserManager.Errors;
internal static class OrganizationErrors
{
    public static Error InvalidOrganizationId =>
        Error.Validation("Не удалось идентифицировать организацию");
    public static Error OrganizationNotFound =>
        Error.NotFound("Организацию не удалось обнаружить");
    public static Error OrganizationFullNameInvalid =>
        Error.Validation("Полное наименование организации должно быть указано");
    public static Error OrganizationShortNameInvalid =>
        Error.Validation("Сокращенное наименование организации должно быть указано");
    public static Error OrganizationOGRNInvalid =>
        Error.Validation("ОГРН должен быть указан и состоять из 13 символов");
    public static Error OrganizationTINInvalid =>
        Error.Validation("ИНН должен быть указан и состоять из 10 символов");
    public static Error OrganizationKPPInvalid =>
        Error.Validation("КПП должен быть указан и состоять из 9 символов");
    public static Error TINAlreadyExists =>
        Error.Conflict("ИНН уже существует");
    public static Error OGRNAlreadyExists =>
        Error.Conflict("ОГРН уже существует");
    public static Error OrganizationUse =>
        Error.BadRequest("Организации не существует или на данный момент в ней состоит хотя бы один человек");
}
