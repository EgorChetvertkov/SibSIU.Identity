using SibSIU.Core.Services.ResultObject;

namespace SibSIU.Domain.ExternalApplication.Errors;
public static class CORSErrors
{
    public static Error InvalidId =>
        Error.Validation("Не удалось определить CORS-политику");
    public static Error CreatorEmpty =>
        Error.Validation("Источник должен быть указан");
    public static Error OriginEmpty =>
        Error.Validation("URL-адрес не может быть пустым");
    public static Error NotFound =>
        Error.NotFound("Не удалось найти CORS-политику");
    public static Error CreatorsNotEquals =>
        Error.Conflict("Изменить политику может только ее создатель");
}
