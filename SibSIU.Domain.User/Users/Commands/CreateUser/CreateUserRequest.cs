using SibSIU.Core.Services;
using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;
using SibSIU.Domain.UserManager.Utils;
using SibSIU.Identity.Models.User.Manage;

using System.Net.Mail;

namespace SibSIU.Domain.UserManager.Users.Commands.CreateUser;
public sealed class CreateUserRequest :
    IRequest<Result<Message>>,
    IRequest<Result<CreateUserResult>>,
    IValidated
{
    public string UserName { get; }
    public string Email { get; }
    public string? PhoneNumber { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public string? Patronymic { get; }
    public DateTimeOffset BirthOfDate { get; }
    public Ulid GenderId { get; }
    public string Password { get; }
    public string ConfirmPassword { get; }

    public CreateUserRequest(CreateUserData userData)
    {
        UserName = userData.UserName.TrimOrEmpty();
        Email = userData.Email.TrimOrEmpty();
        PhoneNumber = userData.PhoneNumber?.Trim();
        FirstName = userData.FirstName.TrimOrEmpty();
        LastName = userData.LastName.TrimOrEmpty();
        Patronymic = userData.Patronymic?.Trim();
        BirthOfDate = userData.BirthOfDate;
        GenderId = userData.Gender.Id;
        Password = userData.Password.TrimOrEmpty();
        ConfirmPassword = userData.ConfirmPassword.TrimOrEmpty();
    }

    public CreateUserRequest() : this(new()) { }

    public Error Validate()
    {
        if(string.IsNullOrWhiteSpace(UserName))
        {
            return UserErrors.InvalidUserName;
        }
        
        if (!MailAddress.TryCreate(Email, out _))
        {
            return UserErrors.InvalidEmail;
        }

        if (PhoneNumber is not null && !PhoneValidation.Validate(PhoneNumber))
        {
            return UserErrors.InvalidPhone;
        }

        if (string.IsNullOrWhiteSpace(FirstName))
        {
            return UserErrors.FirstNameAreEmpty;
        }

        if (string.IsNullOrWhiteSpace(LastName))
        {
            return UserErrors.LastNameAreEmpty;
        }

        if (BirthOfDate.ToUniversalTime().Date.Year - DateTimeOffset.UtcNow.ToUniversalTime().Date.Year >= 14)
        {
            return UserErrors.AgeAreSmall;
        }

        if (GenderId == Ulid.Empty)
        {
            return UserErrors.GenderAreEmpty;
        }

        if (Password.Length < 8)
        {
            return UserErrors.PasswordAreShort;
        }

        if (Password != ConfirmPassword)
        {
            return UserErrors.PasswordNotCompare;
        }

        return Error.None;
    }
}
