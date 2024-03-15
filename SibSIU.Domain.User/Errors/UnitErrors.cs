using SibSIU.Core.Services.ResultObject;

namespace SibSIU.Domain.UserManager.Errors;
public static class UnitErrors
{
    public static Error InvalidUnitId =>
        Error.Unauthorized("Невозможно идентифицировать подразделение");
    public static Error UnitFullNameEmpty =>
        Error.Validation("Подразделение должно содержать полное наименование");
    public static Error UnitShortNameEmpty =>
        Error.Validation("Подразделение должно содержать краткое наименование");
    public static Error UnitParentIdEmpty =>
        Error.Validation("Подразделение должно состоять в другом подразделении");
    public static Error UnitFullNameAlreadyExists =>
        Error.Validation("Полное наименование подразделение уже внесено в систему");
    public static Error UnitShortNameAlreadyExists =>
        Error.Validation("Краткое наименование подразделение уже внесено в систему");
    public static Error UnitParentNotFound =>
        Error.Validation("Подразделение обязано состоять в другом подразделении");
    public static Error UnitHasChildrenOrEmployee =>
        Error.Validation("Подразделение имеет потомков или в нем работает хотя бы один сотрудник");
    public static Error UnitNotFound =>
        Error.Validation("Подразделение не обнаружено");
    public static Error NotFoundSeedUnit =>
        Error.NotFound("Не удалось найти корневое подразделение. Критическая ошибка. Обратитесь к администратору");
}
