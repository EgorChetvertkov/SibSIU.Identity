using SibSIU.Core.Services.ResultObject;

namespace SibSIU.Domain.UserManager.Errors;
public static class ClaimErrors
{
    public static Error ClaimValueEmpty =>
        Error.Validation("Значение утверждения не должно быть пустым");
}
