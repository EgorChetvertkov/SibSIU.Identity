using SibSIU.Core.Services.ResultObject;

namespace SibSIU.Domain.UserManager.Errors;
public static class ScopeErrors
{
    public static Error ScopeNameEmpty =>
        Error.Validation("Наименование области должно быть задано");
    public static Error InvalidScopeId =>
        Error.Validation("Идентификатор области задан не корректно");
    public static Error ScopeNameAlreadyExists =>
        Error.Conflict("Область с таким именем уже существует");
    public static Error ScopeNotFound =>
        Error.NotFound("Область не обнаружена");
    public static Error ClaimTypeUse =>
        Error.BadRequest("Область используется в активных утверждениях или удалена");
}
