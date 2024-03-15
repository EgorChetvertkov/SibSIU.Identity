using SibSIU.Core.Services.ResultObject;

namespace SibSIU.Domain.UserManager.Errors;
public static class PostErrors
{
    public static Error InvalidPostId =>
        Error.Validation("Не удалось идентифицировать должность");
    public static Error PostNotFound =>
        Error.NotFound("Должность не удалось обнаружить");
    public static Error PostAlreadyExists =>
        Error.Conflict("Должность уже существует");
    public static Error PostUseOrEmpty =>
        Error.BadRequest("Должность не существует или на данный момент ее занимает хотя бы один человек");
    public static Error InvalidPostName =>
        Error.Validation("Название должности должно быть указано");
}
