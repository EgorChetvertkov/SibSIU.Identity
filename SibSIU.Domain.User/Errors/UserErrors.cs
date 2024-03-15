using SibSIU.Core.Services.ResultObject;

namespace SibSIU.Domain.UserManager.Errors;
public static class UserErrors
{
    public static Error InvalidUserId =>
        Error.Validation("Идентификатор пользователя указан не верно. Пользователя не существует");
    public static Error InvalidEmail =>
        Error.Validation("Адрес электронной почты указан не верно");
    public static Error EmailNotConfirmed =>
        Error.Failure("Адрес электронной почты не подтвержден");
    public static Error PasswordAreShort =>
        Error.Validation("Пароль должен состоять как минимум из 8 символов");
    public static Error PasswordNotCompare =>
        Error.Validation("Пароли не совпадают");
    public static Error InvalidUserName =>
        Error.Validation("Имя пользователя не может быть пустым");
    public static Error InvalidPhone =>
        Error.Validation("Номер телефона указан не верно");
    public static Error GenderAreEmpty =>
        Error.Validation("Пол пользователя должен быть указан");
    public static Error AgeAreSmall =>
        Error.Validation("Пользователю должно быть как минимум 14 лет");
    public static Error FirstNameAreEmpty =>
        Error.Validation("Имя не должно быть пустым");
    public static Error LastNameAreEmpty =>
        Error.Validation("Фамилия не должно быть пустой");
    public static Error UserNotFound =>
        Error.NotFound("Пользователь не найден");
    public static Error UserNameNotFound =>
        Error.BadRequest("Имя пользователя не обнаружено");
    public static Error InvalidPassword =>
        Error.BadRequest("Пароль указан не верно");
    public static Error UserNameAlreadyExists =>
        Error.Conflict("Имя пользователя уже используется");
    public static Error EmailAlreadyExists =>
        Error.Conflict("Адрес электронной почты уже используется");
    public static Error PhoneAlreadyExists =>
        Error.Conflict("Номер телефона уже используется");
    public static Error GenderNotFound =>
        Error.NotFound("Пол не обнаружен");
    public static Error CountActivePartnersMustBeZeroForAddNew =>
        Error.BadRequest("Для добавления партнера необходимо удалить текущего");
    public static Error CountActivePupilsMustBeZeroForAddNew =>
        Error.BadRequest("Для добавления школьника необходимо удалить текущего");
    public static Error CountActiveStudentsMustBeZeroForAddNew =>
        Error.BadRequest("Для добавления обучающегося необходимо удалить текущего");
    public static Error MessageEmpty =>
        Error.Validation("Сообщение должно быть не пустым");
    public static Error InvalidConfirmationCode =>
        Error.Validation("Код подтверждения не корректен");
}
