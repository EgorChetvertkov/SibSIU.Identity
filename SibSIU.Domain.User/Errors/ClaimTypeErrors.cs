using SibSIU.Core.Services.ResultObject;

namespace SibSIU.Domain.UserManager.Errors;
public static class ClaimTypeErrors
{
    public static Error ClaimTypeNameEmpty =>
        Error.Validation("Наименование типа утверждения не должно быть пустым");
    public static Error InvalidClaimTypeId =>
        Error.Validation("Идентификатор типа утверждения задан не верно");
    public static Error ClaimTypeNameAlreadyExists =>
        Error.Conflict("Имя типа утверждения уже существует");
    public static Error ClaimTypeNotFound =>
        Error.NotFound("Тип утверждения не найден");
    public static Error ClaimTypeUse =>
        Error.BadRequest("Тип утверждения используется в активных утверждениях или не существует и не может быть удален");
}
